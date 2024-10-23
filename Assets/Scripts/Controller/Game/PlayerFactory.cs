using Model;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class PlayerFactory : MonoBehaviour
    {
        [SerializeField] private PlayerBase _playerPrefab;
        [SerializeField] private PlayerBase _AiPlayerPrefab;

        public List<PlayerBase> CreatePlayers(List<PlayerModel> models)
        {
            var result = new List<PlayerBase>();
            models.ForEach(model => result.Add(GetNewPlayer(model)));
            return result;
        }

        public void DestroyPlayer(PlayerBase player)
        {
            player.StopAllCoroutines();
            Destroy(player.gameObject);
        }

        public PlayerBase GetNewPlayer(PlayerModel model)
        {
            PlayerBase result;
            result = model.IsPlayer ? Instantiate(_playerPrefab, transform) : Instantiate(_AiPlayerPrefab, transform);
            result.SetPlayerModel(model);
            return result;
        }
    }
}