using UnityEngine;
using TMPro;

public class IngredientTooltip : MonoBehaviour
{
    public GameObject tooltip;
    public TMP_Text tooltipText;

    public string ingredientName;
    public string ingredientExplanation;

    public void ShowToolTip()
    {
        if (tooltip == null || tooltipText == null)
            return;
        
        if (string.IsNullOrEmpty(ingredientExplanation))
        {
            tooltip.SetActive(false);
            return;
        }

        tooltipText.text = ingredientName + "\n\n" + ingredientExplanation;
        tooltip.SetActive(true);
    }

    public void HideTooltip()
    {
        if (tooltip != null)
        {
            tooltip.SetActive(false);
        }
    }
}
