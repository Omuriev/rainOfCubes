using UnityEngine;

public class CubeStatsUpdater : StatsUpdater<CubeSpawner>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.CreateQuantityChanged += OnCountedCreateObjects;
        _cubeSpawner.ActiveQuantityChanged += OnCountedActiveObjects;
        _cubeSpawner.AllObjectQuantityChanged += OnCountedAllObjects;
    }

    private void OnDisable()
    {
        _cubeSpawner.CreateQuantityChanged -= OnCountedCreateObjects;
        _cubeSpawner.ActiveQuantityChanged -= OnCountedActiveObjects;
        _cubeSpawner.AllObjectQuantityChanged -= OnCountedAllObjects;
    }

    protected override void OnCountedActiveObjects(int count) => SetActiveQuantityText($"Количество активных кубов: {count}");

    protected override void OnCountedCreateObjects(int count) => SetCreatedQuantityText($"Количество созданных кубов: {count}");

    protected override void OnCountedAllObjects(int count) => SetAllObjectsShowedQuantityText($"Количество показанных за все время кубов: {count}");
}
