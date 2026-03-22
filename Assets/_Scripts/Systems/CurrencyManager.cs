using UnityEngine;
using System;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public int currentMoney = 100;

    public event Action<int> OnMoneyChanged;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool CanAfford(int amount)
    {
        return currentMoney >= amount;
    }

    public void Spend(int amount)
    {
        currentMoney -= amount;
        OnMoneyChanged?.Invoke(currentMoney);
    }

    public void Add(int amount)
    {
        currentMoney += amount;
        OnMoneyChanged?.Invoke(currentMoney);
    }
}