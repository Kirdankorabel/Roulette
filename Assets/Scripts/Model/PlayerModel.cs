using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class PlayerModel
    {
        [SerializeField] private int balance;
        [SerializeField] private string name;
        [SerializeField] private bool isPlayer;

        public string Name => name;
        public int Balance => balance;
        public bool IsPlayer => isPlayer;

        public event System.Action<int> OnBalanceChanged;

        public PlayerModel() { }

        public PlayerModel(string name, int balance, bool isPlayer)
        {
            this.name = name;
            this.balance = balance;
            this.isPlayer = isPlayer;
        }

        public void AddMoney(int money)
        {
            balance += money;
            OnBalanceChanged?.Invoke(balance);
        }
    }
}