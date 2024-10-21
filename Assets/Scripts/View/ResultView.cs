using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultView : MonoBehaviour
{
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private TMP_Text _vinnersText;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private string _resultStr = "Result: ";
    [SerializeField] private string _vinnersStr = "Vinners: ";
    [SerializeField] private string _casinoStr = "Casino - ";

    private void Start()
    {
        _acceptButton.onClick.AddListener(Close);
    }

    public void Open(int result, List<BetModel> bets)
    {
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
        _resultText.text = _resultStr + result;
        var sb = new StringBuilder();

        if(bets.Count == 0)
        {
            sb.Append(_vinnersStr).Append(_casinoStr);
            _vinnersText.text = sb.ToString();
        }
        else
        {
            sb.Append(_vinnersStr);
            bets.ForEach(bet => sb.AppendLine($"{bet.PlayerName} - {bet.WinningMoney}"));
            _vinnersText.text = sb.ToString();
        }
    }

    private void Close()
    {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InCubic)
                 .OnComplete(() => gameObject.SetActive(false));
    }
}
