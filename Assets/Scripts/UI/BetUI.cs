using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetUI : MonoBehaviour
{
    [SerializeField] private string _dataName = "BatData";
    [SerializeField] private Button _betButtonPrefab;
    [SerializeField] private Transform _buttonsRoot;
    [SerializeField] private Transform _firstItemPositon;
    [SerializeField] private List<Color> _colors;

    private List<Button> _betButtons = new List<Button>();

    public event System.Action OnBetSeted;

    public static int Bet { get; private set; } = -1;

    void Start()
    {
        Initialize();
    }

    public void ResetBet()
    {
        Bet = -1;
        EnableBetUI(true);
    }

    public void EnableBetUI(bool enabled)
    {
        _betButtons.ForEach(b => b.interactable = enabled);
    }

    public Vector3 GetCellPosition(int target)
    {
        var cell = DataManager.GetData(_dataName).IndexOf(target);
        if(cell >= 0)
        {
            return _betButtons[cell].GetComponent<RectTransform>().position;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private void SetBet(int bet)
    {
        Bet = bet;
        EnableBetUI(true);
        _betButtons[bet].interactable = false;
        OnBetSeted?.Invoke();
    }

    private void Initialize()
    {
        DataManager.GetData(_dataName).ForEach(x => InitializeButton(x));
    }

    private void InitializeButton(int number)
    {
        Button button;
        if (number == 0)
        {
            button = Instantiate(_betButtonPrefab, _firstItemPositon);
            button.transform.localPosition = Vector3.zero;
            button.GetComponent<Image>().color = _colors[0];
        }
        else
        {
            button = Instantiate(_betButtonPrefab, _buttonsRoot);
            button.GetComponent<Image>().color = _colors[number % 2 + 1];
        }
        button.onClick.AddListener(() => SetBet(number));
        button.GetComponentInChildren<TMP_Text>().text = number.ToString();
        _betButtons.Add(button);
    }
}
