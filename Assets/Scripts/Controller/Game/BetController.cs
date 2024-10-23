using Model;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Controller
{
    public class BetController
    {
        private CasinoModel _casinoModel;
        private BetView _betView;
        private List<BetModel> _allBets;
        private List<BetModel> _currentBets;
        private int _count;

        public BetController(BetView betView, CasinoModel casinoModel)
        {
            _betView = betView;
            _casinoModel = casinoModel;
            _allBets = new List<BetModel>();
            _currentBets = new List<BetModel>();
        }

        public void AddBet(BetModel betModel)
        {
            _currentBets.Add(betModel);
            _allBets.Add(betModel);
            _count += betModel.Count;
        }

        public void EndTurn(int result)
        {
            _currentBets.ForEach(bet => bet.SpinResult = result);
            var vinBets = GetVinners(result);
            if (vinBets.Count > 0)
            {
                var totalVinBet = 0f;
                vinBets.ForEach(bet => totalVinBet += bet.Count);
                vinBets.ForEach(bet => AddMoney(bet, _count * (bet.Count / totalVinBet)));
            }
            else
            {
                _casinoModel.AddMoney(_count);
                _betView.AddMoneyToCasino(_count);
            }
            Reset();
            _count = 0;
        }

        public void Reset()
        {
            _currentBets.Clear();
        }

        public List<BetModel> GetVinners(int result)
        {
            return _currentBets.FindAll(b => b.Target == result);
        }

        private void AddMoney(BetModel betModel, float result)
        {
            var intResult = (int)result;
            betModel.WinningMoney = intResult;
            var player = PlayerManager.PlayerControllers.Find(p => p.PlayerName.Equals(betModel.PlayerName));
            if (player == null)
            {
                Debug.LogError(betModel.PlayerName);
            }
            player.AddMoney(intResult);
            _betView.AddMoneyToPlayer(betModel.PlayerName, intResult);
        }
    }
}