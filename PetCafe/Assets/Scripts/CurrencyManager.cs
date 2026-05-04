using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    public int CurrentMoney { get; private set; } = 500;

    public event Action<int> OnMoneyChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Debug.Log(Instance);
    }

    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
        OnMoneyChanged?.Invoke(CurrentMoney);
    }

    public bool SpendMoney(int amount)
    {
        if (CurrentMoney < amount)
            return false;

        CurrentMoney -= amount;
        OnMoneyChanged?.Invoke(CurrentMoney);
        return true;
    }
}