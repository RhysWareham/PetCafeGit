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

    public void SetupMenuItem(PreCookingUI _cookingUI, FoodRecipe recipe, Transform startPos, int offset)
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
        this.transform.position = new Vector2(startPos.position.x + (offset * menuOffset), startPos.position.y);
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
