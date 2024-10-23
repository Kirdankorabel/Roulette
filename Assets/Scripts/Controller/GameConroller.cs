using UnityEngine;

namespace Controller
{
    public class GameConroller : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private TurnController _turnController;

        public static event System.Action OnGameRestared;

        public void StartGame()
        {
            _playerManager.CreatePlayers(GlobalSettings.TotalPlayerCount, GlobalSettings.PlayerCounr);
            _turnController.StartGame();
            OnGameRestared?.Invoke();
        }
    }
}