using UnityEngine;
using UnityEngine.UI;

public class RecipeFilter : MonoBehaviour
{
    public GameObject secondRow;

    public Toggle indonesian;
    public Toggle korean;
    public Toggle indian;
    public Toggle japanese;
    public Toggle filipino;

    public GameObject[] indonesianRecipes;
    public GameObject[] koreanRecipes;
    public GameObject[] indianRecipes;
    public GameObject[] japaneseRecipes;
    public GameObject[] filipinoRecipes;

    public void ButtonClicked()
    {
        secondRow.SetActive(!secondRow.activeSelf);
    }

    public void ApplyFilter()
    {
        // If nothing is selected, show all recipes
        if (!indonesian.isOn &&
            !korean.isOn &&
            !indian.isOn &&
            !japanese.isOn &&
            !filipino.isOn)
        {
            ShowRecipes(indonesianRecipes);
            ShowRecipes(koreanRecipes);
            ShowRecipes(indianRecipes);
            ShowRecipes(japaneseRecipes);
            ShowRecipes(filipinoRecipes);
            return;
        }

        HideAllRecipes();

        if (indonesian.isOn)
            ShowRecipes(indonesianRecipes);

        if (korean.isOn)
            ShowRecipes(koreanRecipes);

        if (indian.isOn)
            ShowRecipes(indianRecipes);

        if (japanese.isOn)
            ShowRecipes(japaneseRecipes);

        if (filipino.isOn)
            ShowRecipes(filipinoRecipes);
    }

    void SetButtonColour(Image button, bool selected)
    {
        Color colour = button.color;

        if (selected)
        {
            // Selected = dark and fully visible
            colour.a = 1f;
            button.color = colour;
            button.transform.localScale = new Vector3(1.08f, 1.08f, 1f);
        }
        else
        {
            // Not selected = faded
            colour.a = 0.45f;
            button.color = colour;
            button.transform.localScale = Vector3.one;
        }
    }

    void HideAllRecipes()
    {
        HideRecipes(indonesianRecipes);
        HideRecipes(koreanRecipes);
        HideRecipes(indianRecipes);
        HideRecipes(japaneseRecipes);
        HideRecipes(filipinoRecipes);
    }

    void HideRecipes(GameObject[] recipes)
    {
        foreach (GameObject recipe in recipes)
        {
            recipe.SetActive(false);
        }
    }

    void ShowRecipes(GameObject[] recipes)
    {
        foreach (GameObject recipe in recipes)
        {
            recipe.SetActive(true);
        }
    }
}