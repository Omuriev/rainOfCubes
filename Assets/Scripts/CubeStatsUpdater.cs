using UnityEngine;

public class CubeStatsUpdater : StatsUpdater<CubeSpawner>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.CreateQuantityChanged += OnCountedCreateObjects;
        _cubeSpawner.ActiveQuantityChanged += OnCountedActiveObjects;
    }

    private void OnDisable()
    {
        _cubeSpawner.CreateQuantityChanged -= OnCountedCreateObjects;
        _cubeSpawner.ActiveQuantityChanged -= OnCountedActiveObjects;
    }

    protected override void OnCountedActiveObjects(int count) => ShowAllActiveObjects.SetActiveCube(count);

    protected override void OnCountedCreateObjects(int count) => SetCreatedQuantityText(" оличество созданных кубов за все врем€ " + count.ToString());
}
