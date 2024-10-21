using TMPro;
using UnityEngine;

public class CasinoView : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void SetModel(CasinoModel model)
    {
        model.OnBalanceChanged += (value) => text.text = value.ToString();
    }
}
