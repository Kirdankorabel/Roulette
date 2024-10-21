using UnityEngine;

public class CasinoModel
{
    [SerializeField] private int balance;

    public int Balance => balance;

    public event System.Action<int> OnBalanceChanged;

    public void AddMoney(int money)
    {
        balance += money;
        OnBalanceChanged?.Invoke(balance);
    }
}
