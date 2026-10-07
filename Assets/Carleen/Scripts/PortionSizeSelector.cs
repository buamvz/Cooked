using TMPro;
using UnityEngine;

public class PortionSizeSelector : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown portionDropdown;
    [SerializeField] private TMP_Text ingredientsText;

    void Start()
    {
        // Reset to 1 person every time the recipe opens
        portionDropdown.value = 0;

        // Show the default ingredients
        UpdateIngredients(0);

        // Listen for dropdown changes
        portionDropdown.onValueChanged.AddListener(UpdateIngredients);
    }

    void UpdateIngredients(int portionIndex)
{
    switch (portionIndex)
    {
        case 0: // 1 person
            ingredientsText.text =
                "ROSE SAUCE\n" +
                "0.75 tbsp gochujang\n" +
                "0.75 tbsp sugar\n" +
                "0.75 tbsp soy sauce\n" +
                "0.5–1 tsp gochugaru\n" +
                "150 ml heavy cream\n" +
                "50 ml whole milk\n\n" +

                "MAIN\n" +
                "0.5 tbsp cooking oil\n" +
                "25 g onion\n" +
                "15 g cabbage\n" +
                "7.5 g green onion\n" +
                "160 g Korean rice cake\n" +
                "50 g Korean fish cake\n" +
                "45 g cocktail sausage\n" +
                "0.25 cup mozzarella cheese\n" +
                "0.5 tbsp parmesan cheese";
            break;

        case 1: // 2 people
            ingredientsText.text =
                "ROSE SAUCE\n" +
                "1.5 tbsp gochujang\n" +
                "1.5 tbsp sugar\n" +
                "1.5 tbsp soy sauce\n" +
                "1–2 tsp gochugaru\n" +
                "300 ml heavy cream\n" +
                "100 ml whole milk\n\n" +

                "MAIN\n" +
                "1 tbsp cooking oil\n" +
                "50 g onion\n" +
                "30 g cabbage\n" +
                "15 g green onion\n" +
                "320 g Korean rice cake\n" +
                "100 g Korean fish cake\n" +
                "90 g cocktail sausage\n" +
                "0.5 cup mozzarella cheese\n" +
                "1 tbsp parmesan cheese";
            break;

        case 2: // 4 people
            ingredientsText.text =
                "ROSE SAUCE\n" +
                "3 tbsp gochujang\n" +
                "3 tbsp sugar\n" +
                "3 tbsp soy sauce\n" +
                "2–4 tsp gochugaru\n" +
                "600 ml heavy cream\n" +
                "200 ml whole milk\n\n" +

                "MAIN\n" +
                "2 tbsp cooking oil\n" +
                "100 g onion\n" +
                "60 g cabbage\n" +
                "30 g green onion\n" +
                "640 g Korean rice cake\n" +
                "200 g Korean fish cake\n" +
                "180 g cocktail sausage\n" +
                "1 cup mozzarella cheese\n" +
                "2 tbsp parmesan cheese";
            break;

        case 3: // 6 people
            ingredientsText.text =
                "ROSE SAUCE\n" +
                "4.5 tbsp gochujang\n" +
                "4.5 tbsp sugar\n" +
                "4.5 tbsp soy sauce\n" +
                "3–6 tsp gochugaru\n" +
                "900 ml heavy cream\n" +
                "300 ml whole milk\n\n" +

                "MAIN\n" +
                "3 tbsp cooking oil\n" +
                "150 g onion\n" +
                "90 g cabbage\n" +
                "45 g green onion\n" +
                "960 g Korean rice cake\n" +
                "300 g Korean fish cake\n" +
                "270 g cocktail sausage\n" +
                "1.5 cups mozzarella cheese\n" +
                "3 tbsp parmesan cheese";
            break;
    }
}
}