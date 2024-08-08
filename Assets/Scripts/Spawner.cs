using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _object;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    protected ObjectPool<T> Pool;
    protected int ObjectCounter = 0;

    public event Action<int> ActiveQuantityChanged;
    public event Action<int> CreateQuantityChanged;
    public event Action<int> AllObjectQuantityChanged;

    private void Awake()
    {
        Pool = new ObjectPool<T>(
        createFunc: () => Instantiate(_object),
        actionOnGet: (obj) => GetAction(obj),
        actionOnRelease: (obj) => obj.gameObject.SetActive(false),
        actionOnDestroy: (obj) => DestroyObject(obj),
        collectionCheck: false,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    public virtual int GetActiveObject() => Pool.CountActive;

    protected void DestroyObject(T obj)
    {
        Destroy(obj);
    }

    protected virtual void GetAction(T obj)
    {
        if (obj.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.velocity = Vector3.zero;
        }

        obj.gameObject.SetActive(true);
    }

    protected virtual T GetObject()
    {
        return Pool.Get();
    }

    protected virtual void OnDisappeared(T obj)
    {
        Pool.Release(obj);
    }

    protected void ChangeObjectCount(int activeCount, int createdCount, int allObjectQuantity)
    {
        ActiveQuantityChanged?.Invoke(Pool.CountActive);
        CreateQuantityChanged?.Invoke(createdCount);
        AllObjectQuantityChanged?.Invoke(allObjectQuantity);
    }
}
