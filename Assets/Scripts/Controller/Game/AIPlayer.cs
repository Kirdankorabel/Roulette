using UnityEngine;
using System.Collections;
using Model;

namespace Controller
{
    public class AIPlayer : PlayerBase
    {
        [SerializeField] private string _betDataName = "BetData";

        public override event System.Action<BetModel> OnBet;

        public override IEnumerator TakeTurnCoroutine()
        {
            yield return new WaitForSeconds(0.5f);

            var values = DataManager.GetData(_betDataName);
            var max = Mathf.Min(_playerModel.Balance / GlobalSettings.ChipSize, GlobalSettings.MaxChipInBet);
            if (GlobalSettings.ChipSize > _playerModel.Balance)
            {
                AddMoney(GlobalSettings.StartBalance);
            }
            var bet = Random.Range(1, max) * GlobalSettings.ChipSize;
            AddMoney(-bet);
            chosenNumber = values[Random.Range(0, values.Count)];
            OnBet?.Invoke(new BetModel(PlayerName, chosenNumber, bet));

            yield break;
        }
    }
}