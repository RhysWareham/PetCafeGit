using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuItem : MonoBehaviour
{
    [SerializeField] private Image foodSprite;
    private CookingUI cookingUI;
    private FoodRecipe currentRecipe;
    private float menuOffset = 300;
    private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI price;

    public void SetupMenuItem(CookingUI _cookingUI, FoodRecipe recipe, Transform startPos, int offset)
    {
        cookingUI = _cookingUI;
        foodSprite.sprite = recipe.icon;
        currentRecipe = recipe;
        price.text = "£" + recipe.price.ToString();
        this.transform.position = new Vector2(startPos.position.x + (offset * menuOffset), startPos.position.y);
    }

    public void RecipeSelected()
    {
        cookingUI.SelectItem(currentRecipe);
    }
}
