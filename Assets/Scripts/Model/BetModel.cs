using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class BetModel
    {
        [SerializeField] private string playerId;
        [SerializeField] private int target;
        [SerializeField] private int count;
        [SerializeField] private int spinResult = -1;
        [SerializeField] private int winningMoney;

        public string PlayerName => playerId;
        public int Target => target;
        public int Count => count;
        public int SpinResult
        {
            get => spinResult;
            set => spinResult = spinResult == -1 ? value : spinResult;
        }
        public int WinningMoney
        {
            get => winningMoney;
            set => winningMoney = value;
        }

        public BetModel() { }

        public BetModel(string playerId, int target, int count)
        {
            this.playerId = playerId;
            this.target = target;
            this.count = count;
        }
    }
}
