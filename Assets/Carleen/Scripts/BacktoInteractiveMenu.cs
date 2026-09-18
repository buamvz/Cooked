using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToInteractiveMenu : MonoBehaviour
{
    [SerializeField] public string sceneName;

    public void GoBack()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}