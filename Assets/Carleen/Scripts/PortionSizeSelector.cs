using TMPro;
using UnityEngine;

public class PortionSizeSelector : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown portionDropdown;
    [SerializeField] private TMP_Text ingredientsText;

    [Header("1 Person")]
    [TextArea(3, 10)]
    [SerializeField] private string ingredients1;

    [Header("2 People")]
    [TextArea(3, 10)]
    [SerializeField] private string ingredients2;

    [Header("4 People")]
    [TextArea(3, 10)]
    [SerializeField] private string ingredients4;

    [Header("6 People")]
    [TextArea(3, 10)]
    [SerializeField] private string ingredients6;

    void Start()
    {
        // Reset to 1 person every time the recipe opens
        portionDropdown.value = 0;

        // Show 1-person ingredients
        UpdateIngredients(0);

        // Listen for dropdown changes
        portionDropdown.onValueChanged.AddListener(UpdateIngredients);
    }

    void UpdateIngredients(int portionIndex)
    {
        switch (portionIndex)
        {
            case 0:
                ingredientsText.text = ingredients1;
                break;

            case 1:
                ingredientsText.text = ingredients2;
                break;

            case 2:
                ingredientsText.text = ingredients4;
                break;

            case 3:
                ingredientsText.text = ingredients6;
                break;
        }
    }
}