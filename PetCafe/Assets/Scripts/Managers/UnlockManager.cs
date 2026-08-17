using UnityEngine;
using System.Collections.Generic;
using System;

public class UnlockManager : MonoBehaviour
{
    public static UnlockManager Instance { get; private set; }

    [SerializeField] private RecipeDatabase recipeDatabase;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    public void HandleLevelUps(int fromLevel, int toLevel)
    {
        if (fromLevel == toLevel)
            return;

        var unlocked = recipeDatabase.GetUnlockedRecipes(fromLevel, toLevel);

        if (unlocked.Count > 0)
        {
            UnlockPopupUI.Instance.Show(unlocked, fromLevel, toLevel);
        }
    }
}