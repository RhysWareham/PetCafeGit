using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuItem : MonoBehaviour
{
    [SerializeField] private Image foodSprite;
    private PreCookingUI cookingUI;
    private FoodRecipe currentRecipe;
    private float menuOffset = 300;
    private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private TextMeshProUGUI timeToCook;
    [SerializeField] private GameObject lockedSprite;
    [SerializeField] private TextMeshProUGUI unlockAtLevel;
    [SerializeField] private Button button;

    public void SetupMenuItem(PreCookingUI _cookingUI, FoodRecipe recipe, int currentLevel)
    {
        cookingUI = _cookingUI;
        foodSprite.sprite = recipe.icon;
        currentRecipe = recipe;
        timeToCook.text = UIUtility.WorkOutTimeInHMS(recipe.cookTime);
        price.text = "£" + recipe.price.ToString();
        if (CurrencyManager.Instance.CurrentMoney < recipe.price)
        {
            price.color = Color.red;
            timeToCook.color = Color.red;
        }

        if (currentLevel < recipe.UnlockLevel)
        {
            lockedSprite.SetActive(true);
            unlockAtLevel.text = $"UNLOCKS AT\nLV {recipe.UnlockLevel}";
            button.enabled = false;
        }
    }

    public void RecipeSelected()
    {
        if (CurrencyManager.Instance.SpendMoney(currentRecipe.price))
        {
            cookingUI.SelectItem(currentRecipe);
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }
}
