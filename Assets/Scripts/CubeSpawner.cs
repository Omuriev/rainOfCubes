using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _repeatRate;
    [SerializeField] private BombSpawner _bombSpawner;

    private void Start() => StartCoroutine(nameof(SpawnCube));

    protected override void GetAction(Cube cube)
    {
        base.GetAction(cube);

        cube.transform.position = GetStartPosition();
        cube.Disappeared += OnDisappeared;
        ObjectCounter++;
        ChangeObjectCount(Pool.CountActive, Pool.CountAll, ObjectCounter);
    }

    protected override void OnDisappeared(Cube cube)
    {
        cube.MeshRenderer.material.color = cube.StartColor;
        cube.Disappeared -= OnDisappeared;
        _bombSpawner.GetBomb(cube.transform.position);
        Pool.Release(cube);
        ChangeObjectCount(Pool.CountActive, Pool.CountAll, ObjectCounter);
    }

    private Vector3 GetStartPosition()
    {
        float positionX = UnityEngine.Random.Range(_startPoint.position.x, _endPoint.position.x);
        float positionY = UnityEngine.Random.Range(_startPoint.position.y, _endPoint.position.y);
        float positionZ = UnityEngine.Random.Range(_startPoint.position.z, _endPoint.position.z);

        return new Vector3(positionX, positionY, positionZ);
    }

    private IEnumerator SpawnCube()
    {
        WaitForSeconds waitTime = new WaitForSeconds(_repeatRate);
        while (true)
        {
            GetObject();
            yield return waitTime;
        }
    }
}
