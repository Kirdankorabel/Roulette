using System.Collections;
using System;
using Model;
using View;

namespace Controller
{
    public class Player : PlayerBase
    {
        public int Balance => _playerModel.Balance;
        public override event Action<BetModel> OnBet;

        public override IEnumerator TakeTurnCoroutine()
        {
            if (Balance < GlobalSettings.ChipSize)
            {
                yield break;
            }

            while (!GameUI.TurnIsEnded)
            {
                yield return null;
            }

            var betcount = GlobalSettings.ChipSize * GameUI.BetCount;
            AddMoney(-betcount);
            var bet = new BetModel(PlayerName, BetUI.Bet, betcount);
            OnBet?.Invoke(bet);

            yield break;
        }
    }
}