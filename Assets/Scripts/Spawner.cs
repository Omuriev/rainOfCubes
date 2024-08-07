using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _object;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    protected ObjectPool<T> _pool;
    protected int ObjectCounter = 0;

    public event Action<int> ActiveQuantityChanged;
    public event Action<int> CreateQuantityChanged;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
        createFunc: () => Instantiate(_object),
        actionOnGet: (obj) => ActionOnGet(obj),
        actionOnRelease: (obj) => obj.gameObject.SetActive(false),
        actionOnDestroy: (obj) => ActionOnDestroy(obj),
        collectionCheck: false,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    public virtual int GetActiveObject() => _pool.CountActive;

    protected void ActionOnDestroy(T obj)
    {
        Destroy(obj);
    }

    protected virtual void ActionOnGet(T obj)
    {
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.gameObject.SetActive(true);
    }

    protected virtual T GetObject()
    {
        return _pool.Get();
    }

    protected virtual void OnDisappeared(T obj)
    {
        _pool.Release(obj);
    }

    protected void ChangeObjectCount(int activeCount, int createdCount)
    {
        ActiveQuantityChanged?.Invoke(_pool.CountActive);
        CreateQuantityChanged?.Invoke(createdCount);
    }
}
