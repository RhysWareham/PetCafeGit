using System;
using UnityEngine;
using System.Collections.Generic;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance { get; private set; }

    public int CurrentLevel { get; private set; } = 1;
    public int CurrentXP { get; private set; }

    public event Action<int, int, int> OnXPChanged;
    // (currentXP, xpToNextLevel, level)

    private List<string> unlockedItemsBuffer = new();

    [SerializeField] private RecipeDatabase recipeDatabase;
    [SerializeField] private int startingLevelMaxXP = 50;
    [SerializeField] private float maxXPNextLevelMultiplier = 1.2f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddXP(int amount)
    {
        CurrentXP += amount;

        while (CurrentXP >= GetXPToNextLevel())
        {
            CurrentXP -= GetXPToNextLevel();
            LevelUp();
        }

        OnXPChanged?.Invoke(CurrentXP, GetXPToNextLevel(), CurrentLevel);
    }

    private void LevelUp()
    {
        CurrentLevel++;
        Debug.Log($"Level Up! Now level {CurrentLevel}");
    }

    public List<string> ConsumeUnlocks()
    {
        List<string> copy = new(unlockedItemsBuffer);
        unlockedItemsBuffer.Clear();
        return copy;
    }


    private int GetXPToNextLevel()
    {
        return Mathf.RoundToInt(startingLevelMaxXP * Mathf.Pow(maxXPNextLevelMultiplier, CurrentLevel - 1));
    }
}
