using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RouletteWheelGenerator : MonoBehaviour
{
    [SerializeField] private Image _sectorPrefab;
    [SerializeField] private Transform _wheelRoot;
    [SerializeField] private float _sectorPadding = 0.01f;
    [SerializeField] private Color _greenColor = Color.green;
    [SerializeField] private Color _redColor = Color.red;
    [SerializeField] private Color _blackColor = Color.black;
    [SerializeField] private string _dataName = "Sectors";

    public float textDistanceFromCenter = 50f;

    void Start()
    {
        GenerateRoulette();
    }

    public void GenerateRoulette()
    {
        var numbers = DataManager.GetData(_dataName);
        var totalSectors = numbers.Count;
        var fillAmountPerSector = 1f / totalSectors;

        Image newSector;
        for (int i = 0; i < totalSectors; i++)
        {
            newSector = Instantiate(_sectorPrefab, _wheelRoot);
            newSector.fillAmount = fillAmountPerSector - _sectorPadding;
            newSector.transform.localRotation = Quaternion.Euler(0, 0, 360f * fillAmountPerSector * i);
            newSector.color = GetSectorColor(i);

            SetSecotrText(newSector, numbers[i], totalSectors);
        }
    }

    private void SetSecotrText(Image newSector, int number, int totalSectors)
    {
        var halfAngleRadians = (180f / totalSectors) * Mathf.Deg2Rad;
        var xShift = Mathf.Sin(halfAngleRadians) * textDistanceFromCenter;
        var offset = new Vector3(-xShift, 0, 0);

        var sectorText = newSector.GetComponentInChildren<TMP_Text>();
        sectorText.text = number.ToString();
        sectorText.alignment = TextAlignmentOptions.Center;

        var textTransform = sectorText.GetComponent<RectTransform>();
        textTransform.anchoredPosition = new Vector3(0, -textDistanceFromCenter) + offset;
        sectorText.transform.localRotation = Quaternion.Euler(0, 0, -180f / totalSectors);

    }

    private Color GetSectorColor(int i)
    {
        if (i == 0)
        {
            return _greenColor;
        }
        else if (i % 2 == 1)
        {
            return _redColor;
        }
        else
        {
            return _blackColor;
        }
    }
}
