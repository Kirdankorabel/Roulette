using Model;
using TMPro;
using UnityEngine;

namespace View
{
    public class CasinoView : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        public void UpdateText(int value)
        {
            text.text = value.ToString();
        }
    }
}