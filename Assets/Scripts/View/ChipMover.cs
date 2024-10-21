using UnityEngine;

public class ChipMover : MonoBehaviour
{
    [SerializeField] private ChipPool _chipPool;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _rectTransform2;

    private static ChipPool _staticChipPool;

    private void Awake()
    {
        _staticChipPool = _chipPool;
    }

    public static void MoveChips(Vector2 startPos, Vector2 endPos, int numberOfObjects)
    {
        Chip chip;
        for (int i = 0; i < numberOfObjects; i++)
        {
            chip = _staticChipPool.GetItem();
            chip.StartMoving(startPos, endPos, true);
        }
    }

    public static void MoveChips(Vector2 startPos, Vector2 endPos, int numberOfObjects, System.Action action)
    {
        Chip chip;
        for (int i = 0; i < numberOfObjects; i++)
        {
            chip = _staticChipPool.GetItem();
            chip.StartMoving(startPos, endPos, true, action);
        }
    }

}
