using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    public void GoToProfileScene() {
        SceneManager.LoadScene("Profile");
    }

    public void GoToDailyRecipesScene() {
        SceneManager.LoadScene("Daily Recipes");
    }

    public void GoToSkillsScene() {
        SceneManager.LoadScene("Skills");
    }
}
