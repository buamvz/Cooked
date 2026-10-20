using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private SceneLoader sceneLoader;
    [SerializeField] private Canvas loginCanvas;

    private void Start()
    {
        // sceneLoader = SceneLoader.Instance;
        sceneLoader = FindAnyObjectByType<SceneLoader>();
    }

    public void PlayAsGuest(string sceneName)
    {
        Debug.Log("Play.");
        sceneLoader.LoadScene(sceneName);
    }

    public void GoToLoginPage()
    {
        Debug.Log("Login and play.");
        sceneLoader.LoadScene("LoginScene");
    }

    public void OpenLoginPanel()
    {
        loginCanvas.gameObject.SetActive(true);
    }

    public void CloseLoginPanel()
    {
        loginCanvas.gameObject.SetActive(false);
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
