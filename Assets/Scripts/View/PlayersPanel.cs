using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class PlayersPanel : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerViewPrefab;
        [SerializeField] private Transform _rootTransform;

        private List<PlayerView> _playerViews = new List<PlayerView>();

        public void DestroyViews()
        {
            _playerViews.ForEach(p => Destroy(p.gameObject));
            _playerViews.Clear();
        }

        public PlayerView GetPlayerView(int index)
        {
            if (_playerViews.Count <= index)
            {
                _playerViews.Add(Instantiate(_playerViewPrefab, _rootTransform));
            }
            return _playerViews[index];
        }

        public PlayerView GetPlayerView(string name)
        {
            return _playerViews.FindLast(p => p.Name == name);
        }
    }
}