using UnityEngine;
using System.Collections.Generic;
using System;

public class UnlockManager : MonoBehaviour
{
    public static UnlockManager Instance { get; private set; }

    [SerializeField] private RecipeDatabase recipeDatabase;

    public event Action<int, int> OnLevelsGained;
    // (fromLevel, toLevel)

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    void Start()
    {
        OnLevelsGained += HandleLevelUps;
    }

    void OnDestroy()
    {
        OnLevelsGained -= HandleLevelUps;
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