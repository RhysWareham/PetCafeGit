using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(menuName = "Cooking/Recipe Database")]
public class RecipeDatabase : ScriptableObject
{
    public RecipeDatabase Instance { get; private set; }

    public List<MachineRecipeSet> allMachines;
    private List<FoodRecipe> _allRecipes;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public List<FoodRecipe> AllRecipes
    {
        get
        {
            if (_allRecipes == null || _allRecipes.Count < 1)
            {
                _allRecipes = allMachines
                    .Where(m => m != null)
                    .SelectMany(m => m.recipes)
                    .Where(r => r != null)
                    .Distinct()
                    .ToList();
            }

            return _allRecipes;
        }
    }

    public List<FoodRecipe> GetRecipesForCategory(Categories.MachineType machineType)
    {
        return allMachines
            .FirstOrDefault(m => m.machineType == machineType)?
            .recipes.OrderBy(r => r.UnlockLevel).ToList();
    }

    public List<FoodRecipe> GetUnlockedRecipes(int fromLevel, int toLevel)
    {
        return AllRecipes
            .Where(r => r.UnlockLevel > fromLevel && r.UnlockLevel <= toLevel)
            .OrderBy(r => r.UnlockLevel)
            .ToList();
    }

}
