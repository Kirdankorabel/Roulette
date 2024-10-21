using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private List<IntDataContainer> _intDataContainers;
    [SerializeField] private List<Sprite> _chips;

    private static List<IntDataContainer> _staticIntDataContainers;
    private static List<Sprite> _staticChips;
    private static Sprite _staticChipSprite;

    public static event System.Action OuChipUpdated;

    private void Awake()
    {
        _staticIntDataContainers = _intDataContainers;
        _staticChips = _chips;
    }

    private void OnEnable()
    {
        _staticIntDataContainers = _intDataContainers;
    }

    public static List<int> GetData(string name)
    {
        return _staticIntDataContainers.FindLast(x => x.Name == name).Values;
    }

    public static void SetChipSprite(int index)
    {
        _staticChipSprite = _staticChips[index];
        OuChipUpdated?.Invoke();
    }

    public static Sprite GetChipSprite()
    {
        return _staticChipSprite;
    }
}
