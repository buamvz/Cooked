using System.Collections;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProfilePageManager : MonoBehaviour
{
    [SerializeField] private GameObject inputField;
    public bool isEditing;

    [SerializeField] private TMP_Text usernameField;
    [SerializeField] private TMP_Text levelsCompletedField;
    [SerializeField] private TMP_Text totalPointsField;
    [SerializeField] private TMP_Text playerIDField;

    private void Awake()
    {
        inputField.SetActive(false);
        UpdateProfileInfo();
    }


    // === Updating player username ===
    public void StartEditingUsername()
    {
        StartCoroutine(EditUsernameFunction());
    }

    public IEnumerator EditUsernameFunction()
    {
        isEditing = true;
        inputField.SetActive(true);
        while (isEditing)
        {
            if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
            {
                SaveUsernameToProfile();
                yield break;
            }

            yield return null;
        }
        yield return null;
    }

    private async void SaveUsernameToProfile()
    {
        string newUsername = inputField.GetComponent<TMP_InputField>().text;

        PlayerDataManager.Instance.playerProfile.playerName = newUsername;

        Debug.Log("New Username: " + newUsername);
        usernameField.text = newUsername;

        await PlayerDataManager.Instance.SavePlayerData();

        inputField.SetActive(false);
        isEditing = false;
    }

    // === Update other information on load in ===
    public void UpdateProfileInfo()
    {
        if (PlayerDataManager.Instance.playerProfile != null)
        {
            if (PlayerDataManager.Instance.playerProfile.playerName != null)
            {

                usernameField.text = PlayerDataManager.Instance.playerProfile.playerName;
            }
            else
            {
                usernameField.text = "NoUserName";
            }

            levelsCompletedField.text = PlayerDataManager.Instance.playerProfile.completedLevels.ToString();
            totalPointsField.text = PlayerDataManager.Instance.playerProfile.totalScore.ToString();
            playerIDField.text = AuthenticationService.Instance.PlayerId;
        }
        else
        {
            Debug.LogWarning("Player profile is null. Cannot update profile info.");
        }
    }

}
