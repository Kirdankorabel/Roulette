using Controller;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Button _rollbutton;
        [SerializeField] private Button _endTurnButton;
        [SerializeField] private Slider _betSlider;
        [SerializeField] private Slider _ballSlider;
        [SerializeField] private TMP_Text _betText;
        [SerializeField] private RouletteSpinner _rouletteSpinner;
        [SerializeField] private BallSpinner _ballSpinner;
        [SerializeField] private BetUI _betUI;
        [SerializeField] private RollController _rollController;
        [SerializeField] private string _dataName = "Sectors";

        private int _result = -1;

        public event System.Action OnSpinEnded;
        public event System.Action<float> OnBallPositionUpdated;

        public static bool TurnIsEnded { get; private set; }
        public static int BetCount { get; private set; }
        public static float BallPosition { get; private set; }

        void Start()
        {
            _rollController.OnRolled += StartRolling;
            _rollbutton.onClick.AddListener(_rollController.Roll);
            _endTurnButton.onClick.AddListener(EndTutn);
            _betUI.OnBetSeted += EnableTurnButton;
            _betSlider.onValueChanged.AddListener(UpdateBetText);
            _ballSlider.onValueChanged.AddListener(UpdateBallPosition);
        }

        public void ResetUIForPlayer(Player player)
        {
            BetCount = 1;
            _betUI.ResetBet();
            _betUI.EnableBetUI(true);
            _endTurnButton.interactable = false;
            _betText.text = GlobalSettings.ChipSize.ToString();
            _betSlider.value = 1;
            _betSlider.maxValue = Mathf.Min(player.Balance / GlobalSettings.ChipSize, GlobalSettings.MaxChipInBet);
            _betSlider.interactable = true;
            TurnIsEnded = false;
            _result = -1;
        }

        public void EnableTurnUI()
        {
            TurnIsEnded = false;
            _betUI.EnableBetUI(false);
            _betSlider.interactable = false;
        }

        public void EnableRollButton()
        {
            _rollbutton.interactable = true;
            _ballSlider.gameObject.SetActive(true);
            _ballSlider.value = 0;
        }

        private void StartRolling(int result)
        {
            _result = DataManager.GetData(_dataName)[result];
            _rouletteSpinner.StartSpin();
            _ballSpinner.StartBallSpin(result);
            _rollbutton.interactable = false;
            _ballSlider.gameObject.SetActive(false);
            StartCoroutine(WaiteRouletteCorutine());
        }

        private void UpdateBallPosition(float value)
        {
            BallPosition = value;
            _ballSpinner.SetBallPosition(value);
        }

        private void UpdateBetText(float value)
        {
            BetCount = (int)value;
            _betText.text = (value * GlobalSettings.ChipSize).ToString();
        }

        private void EnableTurnButton()
        {
            _endTurnButton.interactable = true;
        }

        private void EndTutn()
        {
            TurnIsEnded = true;
            _endTurnButton.interactable = false;
        }


        private IEnumerator WaiteRouletteCorutine()
        {
            while (!_rouletteSpinner.IsStopped || BallSpinner.IsBallSpinning)
            {
                yield return null;
            }
            OnSpinEnded?.Invoke();
        }
    }
}