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
        // Change toggle colours
        SetButtonColour(indonesian, indonesian.isOn);
        SetButtonColour(korean, korean.isOn);
        SetButtonColour(indian, indian.isOn);
        SetButtonColour(japanese, japanese.isOn);
        SetButtonColour(filipino, filipino.isOn);

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

    void SetButtonColour(Toggle button, bool selected)
    {
        Image image = button.GetComponent<Image>();

        if (selected)
        {
            // Selected = #25CCEE
            Color colour;
            ColorUtility.TryParseHtmlString("#25CCEE", out colour);
            image.color = colour;
        }
        else
        {
            // Not selected = white
            image.color = Color.white;
        }
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