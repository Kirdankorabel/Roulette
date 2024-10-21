using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Chip : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _image;
    [SerializeField] private float _maxDeviation = 50f;
    [SerializeField] private float _moveTime = 1.0f;

    public event System.Action<Chip> OnChipDisabled;

    private void OnEnable()
    {
        _image.sprite = DataManager.GetChipSprite();
    }

    public void StartMoving(Vector2 startPos, Vector2 endPos, bool disableChip)
    {
        StartCoroutine(MoveArcCorutine(startPos, endPos, disableChip, null));
    }

    public void StartMoving(Vector2 startPos, Vector2 endPos, bool disableChip, System.Action action)
    {
        StartCoroutine(MoveArcCorutine(startPos, endPos, disableChip, action));
    }

    private IEnumerator MoveArcCorutine(Vector2 startPos, Vector2 endPos, bool disableChip, System.Action action)
    {
        _rectTransform.position = startPos;

        var speed = GlobalSettings.ChipSpeed + Random.Range(-GlobalSettings.SpeedDispersion, GlobalSettings.SpeedDispersion);
        var midPoint = (startPos + endPos) / 2;
        midPoint += Vector2.up * Random.Range(0, _maxDeviation);
        var elapsedTime = 0f;
        var t = 0f;
        Vector2 arcPoint;

        while (elapsedTime < _moveTime)
        {
            t = elapsedTime / _moveTime;
            arcPoint = Vector2.Lerp(Vector2.Lerp(startPos, midPoint, t), Vector2.Lerp(midPoint, endPos, t), t);
            _rectTransform.position = arcPoint;
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }

        action?.Invoke();
        _rectTransform.position = endPos;

        if (disableChip)
        {
            OnChipDisabled?.Invoke(this);
        }
    }
}
