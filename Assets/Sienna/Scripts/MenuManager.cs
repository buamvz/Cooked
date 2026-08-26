using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = SceneLoader.Instance;
    }

    public void PlayAsGuest()
    {
        Debug.Log("Play as guest.");
        sceneLoader.LoadScene("Minigame");
    }

    public void GoToLoginPage()
    {
        Debug.Log("Login and play.");
        sceneLoader.LoadScene("LoginScene");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
        #else
                Application.Quit();
        #endif

    }
}
