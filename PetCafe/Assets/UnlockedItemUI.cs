using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockedItemUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;

    public void Setup(FoodRecipe recipe)
    {
        icon.sprite = recipe.icon;
        nameText.text = recipe.name;
    }
}

