using Model;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Controller
{
    public class PlayerManager : MonoBehaviour
    {
        public static List<PlayerBase> PlayerControllers;

        [SerializeField] private PlayerFactory _playerFactory;
        [SerializeField] private PlayersPanel _playersPanel;

        public static int GetPlayerColor(string playerName)
        {
            return PlayerControllers.FindIndex(p => p.PlayerName == playerName);
        }

        public void CreatePlayers(int totalPlayerCount, int playerCount)
        {
            var counter = 0;
            var playerModels = PlayerModelFactory.CreatePlayers(totalPlayerCount, playerCount);
            if (PlayerControllers != null && PlayerControllers.Count > 0)
            {
                PlayerControllers.ForEach(p => _playerFactory.DestroyPlayer(p));
                _playersPanel.DestroyViews();
            }
            PlayerControllers = _playerFactory.CreatePlayers(playerModels);
            PlayerControllers.ForEach(p => p.SetView(_playersPanel.GetPlayerView(counter++)));
        }
    }
}