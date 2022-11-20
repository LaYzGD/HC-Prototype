using UnityEngine;
using System;

[DefaultExecutionOrder(0)]
public class MoneySystem : MonoBehaviour
{
    private int _totalCoins;

    public int TotalCoins => _totalCoins;

    public delegate void OnUpdateCoinsDelegate();

    public OnUpdateCoinsDelegate OnUpdateCoins;

    public static MoneySystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public bool TryAddCoins(int amount)
    {
        if (amount > 0)
        {
            _totalCoins += amount;
            OnUpdateCoins?.Invoke();
            return true;
        }
       
        return false;
    }

    public bool TrySubtractCoins(int amount)
    {
        if (amount > 0 && amount <= _totalCoins)
        {
            _totalCoins -= amount;
            OnUpdateCoins?.Invoke();
            return true;
        }

        return false;
    }
}
