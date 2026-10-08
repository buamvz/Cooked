using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    public Action PlayerSignedIn;

    [SerializeField] private SceneLoader sceneLoader;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        if (!AuthenticationService.Instance.SessionTokenExists)
        {
            Debug.Log("Session Token not found.");
            return;
        }

        Debug.Log("Returning player siging in...");
        await SignInAnonymouslyAsync();

        // sceneLoader = FindAnyObjectByType<SceneLoader>();
    }

    private async void Awake()
    {
        if (UnityServices.State == ServicesInitializationState.Uninitialized)
        {
            try
            {
                await UnityServices.InitializeAsync();

                Debug.Log("Services Initializing");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to initialize Unity Services: {ex}");
                return;
            }
            await UnityServices.InitializeAsync();
        }

        PlayerAccountService.Instance.SignedIn += SignInOrLinkWithUnity;
    }

    // scene load to game
    public void PlayGame()
    {
        Debug.Log("Sign in and Play");
        sceneLoader.LoadScene("Level Map");
    }

    public async void StartAnonymousSignIn()
    {
        await SignInAnonymouslyAsync();
    }

    public async void StartUnitySignInAsync()
    {
        if (PlayerAccountService.Instance.IsSignedIn)
        {
            SignInOrLinkWithUnity();
            PlayGame();
            return;
        }

        try
        {
            await PlayerAccountService.Instance.StartSignInAsync();
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }

    private async void SignInOrLinkWithUnity()
    {
        try
        {
            // 1. Player is not yet authenticated, signing up with unity
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                Debug.Log("Signing up with Unity Player Account");
                await AuthenticationService.Instance.SignInWithUnityAsync(PlayerAccountService.Instance.AccessToken);
                Debug.Log("Successfully signed up with Unity Player Account");

                await InitializePlayer();

                return;
            }
            // 2. Player is authenticated, but does not yet have unity ID linked, so lets link
            if (!HasUnityID())
            {
                Debug.Log("Linking anonymous account to Unity...");
                await LinkWithUnityAsync(PlayerAccountService.Instance.AccessToken);
                Debug.Log("Successfully linked with anonymous account!");

                await InitializePlayer();

                return;
            }

            // 3. Player has authentication and a Unity ID
            Debug.Log("Player is already signed in to their Unity Player Account");

            await InitializePlayer();
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }

    private bool HasUnityID()
    {
        return AuthenticationService.Instance.PlayerInfo.GetUnityId() != null;
    }

    private async Task SignInAnonymouslyAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sign in anonymously succeeded!");

            // Shows the playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

            await InitializePlayer();
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    private async Task LinkWithUnityAsync(string accessToken)
    {
        try
        {
            await AuthenticationService.Instance.LinkWithUnityAsync(accessToken);
            Debug.Log("Link is successful.");
        }
        catch (AuthenticationException ex) when (ex.ErrorCode == AuthenticationErrorCodes.AccountAlreadyLinked)
        {
            // Prompt the player with an error message.
            Debug.LogError("This user is already linked with another account. Log in instead.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    public void SignOut()
    {
        try
        {
            // sign out of authentication service
            if (AuthenticationService.Instance.IsSignedIn)
            {
                AuthenticationService.Instance.SignOut();
                Debug.Log("Player signed out of Unity Authentication Service");
            }
            // sign out of player account service
            if (PlayerAccountService.Instance.IsSignedIn)
            {
                PlayerAccountService.Instance.SignOut();
                Debug.Log("Player signed out of Unity Player Account Service");
            }

            PlayerPrefs.DeleteKey("UnityMessaginServices.SessionToken");
            PlayerPrefs.Save();

            Debug.Log("Player signed out successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error during signout: {ex.Message}");
        }
    }

    // function for initializing/loading player profile data
    private async Task InitializePlayer()
    {
        Debug.Log("Loading player profile data...");

        // load player data
        await PlayerDataManager.Instance.LoadPlayerData();
        
        Debug.Log("Player profile data loaded successfully.");
        PlayerSignedIn?.Invoke();
    }

}
