using System.Collections;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    protected PlayerModel _playerModel;
    protected PlayerView _playerView;

    public string PlayerName => _playerModel.Name;

    protected int chosenNumber = -1;

    public abstract event System.Action<BetModel> OnBet;

    public void SetPlayerModel(PlayerModel model)
    {
        _playerModel = model;
    }

    public void SetView(PlayerView view)
    {
        view.UpdateView(PlayerName, _playerModel.Balance);
        _playerModel.OnBalanceChanged += view.UpdateBalance;
    }

    public void AddMoney(int count)
    {
        _playerModel.AddMoney(count);
    }

    public abstract IEnumerator TakeTurnCoroutine();
}
