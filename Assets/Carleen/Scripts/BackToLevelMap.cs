using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToLevelMap : MonoBehaviour
{
    public void BackToMap()
    {
        SceneManager.LoadScene("Level Map");
    }
}