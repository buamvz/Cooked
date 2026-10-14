using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenRecipeButton : MonoBehaviour
{
    [SerializeField] public string sceneName;
    public void LoadSceneByName(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log("Loading:" + sceneName);
        }
        else
        {
            Debug.Log("Scene empty");
        }

    }
}
