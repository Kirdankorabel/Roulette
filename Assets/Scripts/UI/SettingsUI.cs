using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Transform _panelTransform;
    [SerializeField] private Image _image;
    [SerializeField] private Slider _totalPlayerCountSlider;
    [SerializeField] private Slider _playerCountSlider;
    [SerializeField] private Slider _betSlider;
    [SerializeField] private Slider _startBalanceSlider;
    [SerializeField] private TMP_Text _totalPlayerCountText;
    [SerializeField] private TMP_Text _playerCountText;
    [SerializeField] private TMP_Text _betText;
    [SerializeField] private TMP_Text _balanceText;
    [SerializeField] private string _totalPlayerCountStr = "Total player count: ";
    [SerializeField] private string _playerCountStr = "Player count: ";
    [SerializeField] private string _betStr = "Chip size: ";
    [SerializeField] private string _balanceStr = "Start balance: ";
    [SerializeField] private Button _startNewGameButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _openSettingsButton;
    [SerializeField] private List<int> _bets;
    [SerializeField] private List<int> _balances;
    [SerializeField] private GameConroller _gameConroller;

    private void Start()
    {
        Construct();
    }

    public void Construct()
    {
        _betSlider.maxValue = _bets.Count - 1;
        _totalPlayerCountSlider.onValueChanged.AddListener(UpdateTotalPlayerCount);
        _playerCountSlider.onValueChanged.AddListener(UpdatePlayerCount);
        _betSlider.onValueChanged.AddListener(UpdateBet);
        _startBalanceSlider.onValueChanged.AddListener(UpdateStartBalance);

        _startNewGameButton.onClick.AddListener(RestartGame);
        _closeButton.onClick.AddListener(Clsoe);
        _openSettingsButton.onClick.AddListener(() => Open(true));
        _quitButton.onClick.AddListener(Application.Quit);

        _totalPlayerCountSlider.value = 5;
        _playerCountSlider.value = 1;
        _betSlider.value = 1;
        _startBalanceSlider.value = 2;
    }

    private void Open(bool value)
    {
        gameObject.SetActive(true);
        _closeButton.gameObject.SetActive(value);
        _panelTransform.localScale = Vector3.zero;
        _panelTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutQuad);
        _image.DOFade(.4f, 0.5f).SetEase(Ease.InOutQuad);
    }

    private void Clsoe()
    {
        _image.DOFade(0, 0.5f).SetEase(Ease.InOutQuad);
        _panelTransform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutQuad)
                       .OnComplete(() => gameObject.SetActive(false));
    }

    private void RestartGame()
    {
        _image.DOFade(0, 0.5f).SetEase(Ease.InOutQuad);
        _panelTransform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutQuad)
                       .OnComplete(() =>
                       {
                           _gameConroller.StartGame();
                           gameObject.SetActive(false); 
                       });
    }

    private void UpdateTotalPlayerCount(float count)
    {
        _totalPlayerCountText.text = _totalPlayerCountStr +((int)count).ToString();
        GlobalSettings.TotalPlayerCount = (int)count;
        _playerCountSlider.value = Mathf.Min(_playerCountSlider.value, count);
        _playerCountSlider.maxValue = (int)count;
    }

    private void UpdatePlayerCount(float count)
    {
        _playerCountText.text = _playerCountStr + ((int)count).ToString();
        GlobalSettings.PlayerCounr = (int)count;
    }

    private void UpdateStartBalance(float count)
    {
        var index = (int)count;
        _balanceText.text = _balanceStr + (_balances[index]).ToString();
        GlobalSettings.StartBalance = _balances[index];
    }

    private void UpdateBet(float count)
    {
        var index = (int)count;
        DataManager.SetChipSprite(index);
        _betText.text = _betStr + (_bets[index]).ToString();
        GlobalSettings.ChipSize = _bets[index];
    }
}
