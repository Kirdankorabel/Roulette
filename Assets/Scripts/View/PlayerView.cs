using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _balanceText;
        [SerializeField] private Image _chipImage;

        private string _name;

        public string Name => _name;

        public void UpdateView(string name, int balance)
        {
            _name = name;
            _nameText.text = name;
            _balanceText.text = balance.ToString();
            _chipImage.sprite = DataManager.GetChipSprite();
        }

        public Vector3 GetChipPosition()
        {
            return _chipImage.transform.position;
        }

        public void UpdateBalance(int balance)
        {
            _balanceText.text = balance.ToString();
        }
    }
}