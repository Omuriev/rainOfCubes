using System;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _repeatRate;
    [SerializeField] private BombSpawner _bombSpawner;

    private void Start() => InvokeRepeating(nameof(GetObject), 0.0f, _repeatRate);

    protected override void ActionOnGet(Cube obj)
    {
        base.ActionOnGet(obj);

        obj.transform.position = GetStartPosition();
        obj.Disappearing += OnDisappeared;
        ObjectCounter++;
        ChangeObjectCount(_pool.CountActive, _pool.CountAll);
    }

    protected override void OnDisappeared(Cube cube)
    {
        cube.MeshRenderer.material.color = cube.StartColor;
        cube.Disappearing -= OnDisappeared;
        _bombSpawner.GetBomb(cube.transform.position);
        _pool.Release(cube);
        ChangeObjectCount(_pool.CountActive, _pool.CountAll);
    }

    private Vector3 GetStartPosition()
    {
        float positionX = UnityEngine.Random.Range(_startPoint.position.x, _endPoint.position.x);
        float positionY = UnityEngine.Random.Range(_startPoint.position.y, _endPoint.position.y);
        float positionZ = UnityEngine.Random.Range(_startPoint.position.z, _endPoint.position.z);

        return new Vector3(positionX, positionY, positionZ);
    }
}
