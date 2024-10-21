using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetView : MonoBehaviour
{
    [SerializeField] private BetUI _betUI;
    [SerializeField] private List<Color> _colorList;
    [SerializeField] private List<Image> _markers;
    [SerializeField] private List<Vector3> _offets;
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private ChipPool _chipPool;
    [SerializeField] private Image _chipImage;
    [SerializeField] private Image _casinoChipImage;
    [SerializeField] private PlayersPanel _playersPanel;
    [SerializeField] private float _radius = 50;

    private int _counter;

    private void Awake()
    {
        GameConroller.OnGameRestared += ResetBetView;
        DataManager.OuChipUpdated += UpdateImages;
    }

    public void ShowBet(BetModel betModel)
    {
        var playerColor = PlayerManager.GetPlayerColor(betModel.PlayerName);
        if(playerColor < 0)
        {
            return;
        }
        var chip = _markers[playerColor];
        chip.gameObject.SetActive(true);
        chip.color = _colorList[playerColor];
        chip.transform.position = _betUI.GetCellPosition(betModel.Target) + _offets[playerColor];

        ChipMover.MoveChips(_playersPanel.GetPlayerView(betModel.PlayerName).GetChipPosition(),
                              _chipImage.transform.position,
                              betModel.Count / GlobalSettings.ChipSize, IncreaceCountText);
    }

    public void EnableMarkers(bool enable)
    {
        _markers.ForEach(x => x.gameObject.SetActive(enable));
    }

    public void AddMoneyToCasino(int count)
    {
        ChipMover.MoveChips(_chipImage.transform.position,
                            _casinoChipImage.transform.position,
                            count / GlobalSettings.ChipSize);

        _counter = 0;
        _countText.text = _counter.ToString();
    }

    private void ResetBetView()
    {
        EnableMarkers(false);
        _countText.text = string.Empty;
        _counter = 0;
    }

    public void AddMoneyToPlayer(string playerName, int money)
    {
        ChipMover.MoveChips(_chipImage.transform.position, 
                            _playersPanel.GetPlayerView(playerName).GetChipPosition(),
                            money / GlobalSettings.ChipSize);
        _counter -= money;

        if(_counter < 10)
        {
            _counter = 0;
        }

        _countText.text = _counter.ToString();
    }

    private void UpdateImages()
    {
        _chipImage.sprite = DataManager.GetChipSprite();
        _casinoChipImage.sprite = DataManager.GetChipSprite();
    }

    private void IncreaceCountText()
    {
        _counter += GlobalSettings.ChipSize;
        _countText.text = _counter.ToString();
    }
}
