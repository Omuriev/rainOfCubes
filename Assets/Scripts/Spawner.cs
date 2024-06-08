using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _repeatRate;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private ObjectPool<Cube> _cubePool;

    private void OnDisappeared(Cube cube)
    {
        cube.Disappearing -= OnDisappeared;
        cube.MeshRenderer.material.color = cube.StartColor;
        cube.ResetLifeTime();
        _cubePool.Release(cube);
    }

    private void Awake()
    {
        _cubePool = new ObjectPool<Cube>(
        createFunc: () => Instantiate(_cubePrefab),
        actionOnGet: (obj) => ActionOnGet(obj),
        actionOnRelease: (obj) => obj.gameObject.SetActive(false),
        actionOnDestroy: (obj) => Destroy(obj),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private void ActionOnGet(Cube cube)
    {
        cube.transform.position = GetStartPosition();
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cube.gameObject.SetActive(true);
        cube.Disappearing += OnDisappeared;
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetSphere), 0.0f, _repeatRate);
    }

    private void GetSphere()
    {
        _cubePool.Get();
    }

    private Vector3 GetStartPosition()
    {
        float positionX = UnityEngine.Random.Range(_startPoint.position.x, _endPoint.position.x);
        float positionY = UnityEngine.Random.Range(_startPoint.position.y, _endPoint.position.y);
        float positionZ = UnityEngine.Random.Range(_startPoint.position.z, _endPoint.position.z);

        return new Vector3(positionX, positionY, positionZ);
    }
}
