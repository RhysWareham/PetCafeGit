using UnityEngine;

[CreateAssetMenu(menuName = "Cooking/Food Recipe")]
public class FoodRecipe : ScriptableObject
{
    public string foodName;
    public Sprite icon;
    public Sprite cookingSprite;
    public Sprite cookedSprite;

    public Categories.CookingCategory category;
    public int price;
    public int cookTime;
    public int xpReward;
    public int requiredLevelToCook;
    public int maxMeals;
    public int profitPerMeal;
}
