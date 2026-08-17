using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(menuName = "Furniture/Furniture Database")]
public class FurnitureDatabase : ScriptableObject
{
    public FurnitureDatabase Instance { get; private set; }

    public List<FurnitureSet> allFurniture;
    private List<PlaceableData> _allFurniture;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public List<PlaceableData> AllFurniture
    {
        get
        {
            if (_allFurniture == null || _allFurniture.Count < 1)
            {
                _allFurniture = allFurniture
                    .Where(m => m != null)
                    .SelectMany(m => m.placeables)
                    .Where(r => r != null)
                    .Distinct()
                    .ToList();
            }

            return _allFurniture;
        }
    }

    public List<PlaceableData> GetRecipesForCategory(Categories.FurnitureType furnitureType)
    {
        return allFurniture
            .FirstOrDefault(m => m.furnitureType == furnitureType)?
            .placeables.OrderBy(r => r.UnlockLevel).ToList();
    }

    public List<PlaceableData> GetUnlockedRecipes(int fromLevel, int toLevel)
    {
        return AllFurniture
            .Where(r => r.UnlockLevel > fromLevel && r.UnlockLevel <= toLevel)
            .OrderBy(r => r.UnlockLevel)
            .ToList();
    }

}
