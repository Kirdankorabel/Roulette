using Model;
using UnityEngine;
using View;

namespace Controller
{
    public class TurnController : MonoBehaviour
    {
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private BetView _betView;
        [SerializeField] private CasinoView _casinoView;
        [SerializeField] private ResultView _resultView;
        [SerializeField] private string _dataName = "Sectors";

        private BetController _betController;
        private int currentPlayerIndex = 0;


        void Start()
        {
            Initialize();
            _gameUI.OnSpinEnded += EndRound;
        }

        public void StartGame()
        {
            PlayerManager.PlayerControllers.ForEach(player => player.OnBet += PlayerBetHeandler);
            currentPlayerIndex = 0;
            StartNexTurn();
            _betController.Reset();
        }

        private void EndRound()
        {
            var result = RollController.LastResult;
            _betController.EndTurn(result);
            _betView.EnableMarkers(false);
            _resultView.Open(DataManager.GetData(_dataName)[result], _betController.GetVinners(result));
            StartNexTurn();
        }

        private void Initialize()
        {
            var casino = new CasinoModel();
            casino.OnBalanceChanged += _casinoView.UpdateText;
            _betController = new BetController(_betView, casino);
        }

        private void PlayerBetHeandler(BetModel betModel)
        {
            _betView.ShowBet(betModel);
            _betController.AddBet(betModel);
            StartNexTurn();
        }

        private void StartNexTurn()
        {
            if (currentPlayerIndex < PlayerManager.PlayerControllers.Count)
            {
                if (PlayerManager.PlayerControllers[currentPlayerIndex] is Player)
                {
                    _gameUI.ResetUIForPlayer(PlayerManager.PlayerControllers[currentPlayerIndex] as Player);
                }
                else
                {
                    _gameUI.EnableTurnUI();
                }
                StartCoroutine(PlayerManager.PlayerControllers[currentPlayerIndex].TakeTurnCoroutine());
                currentPlayerIndex++;
            }
            else
            {
                _gameUI.EnableRollButton();
                currentPlayerIndex = 0;
            }
        }
    }
}