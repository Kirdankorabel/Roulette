using UnityEngine;
using UnityEngine.Pool;

public class Pool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 100;

    private ObjectPool<T> _pool;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(
            InstantiateItem,
            OnGet,
            OnReleas,
            OnDestroyElement,
            false,
            _defaultCapacity,
            _maxSize);
    }

    public virtual T GetItem(string name)
    {
        var item = Instantiate(_prefab);
        return item;
    }


    public virtual T GetItem()
    {
        return _pool.Get();
    }

    public virtual void ReleaseItem(T dObject)
    {
        _pool.Release(dObject);
    }

    protected virtual void OnApplicationQuit()
    {
        _pool.Dispose();
    }

    #region pool methods
    protected virtual T InstantiateItem()
    {
        var item = Instantiate(_prefab);
        return item;
    }

    protected virtual void OnGet(T gameObject)
    {
        gameObject.gameObject.SetActive(true);
        gameObject.transform.parent = transform;
    }

    protected virtual void OnReleas(T gameObject)
    {
        gameObject.gameObject.SetActive(false);
        gameObject.transform.parent = transform;
    }
    protected virtual void OnDestroyElement(T gameObject) { }
    #endregion
}
