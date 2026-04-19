using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Cooking/RecipeDatabase")]
public class RecipeDatabase : ScriptableObject
{
    public List<FoodRecipe> allRecipes;
}
