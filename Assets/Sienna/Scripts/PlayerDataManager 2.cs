using Unity.Services.CloudCode;
using Unity.Services.CloudCode.GeneratedBindings;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public MyModuleBindings MyModuleBindings;
    public LoginManager LoginManager;
    public string PlayerName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoginManager.PlayerSignedIn += InitializePlayer;

        MyModuleBindings = new MyModuleBindings(CloudCodeService.Instance);
    }

    public async void InitializePlayer()
    {
        try
        {
            var resultFromCloud = await MyModuleBindings.SayHello(PlayerName);
            Debug.Log($"{resultFromCloud}");
        }
        catch (CloudCodeException ex)
        {
            Debug.LogException(ex);
        }
    }

    private void OnDisable()
    {
        LoginManager.PlayerSignedIn -= InitializePlayer;
    }
}
