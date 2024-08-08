using System;
using UnityEngine;

public class BombStatsUpdater : StatsUpdater<BombSpawner>
{
    [SerializeField] private BombSpawner _bombSpawner;

    private void OnEnable()
    {
        _bombSpawner.CreateQuantityChanged += OnCountedCreateObjects;
        _bombSpawner.ActiveQuantityChanged += OnCountedActiveObjects;
        _bombSpawner.AllObjectQuantityChanged += OnCountedAllObjects;
    }

    private void OnDisable()
    {
        _bombSpawner.CreateQuantityChanged -= OnCountedCreateObjects;
        _bombSpawner.ActiveQuantityChanged -= OnCountedActiveObjects;
        _bombSpawner.AllObjectQuantityChanged += OnCountedAllObjects;
    }

    protected override void OnCountedActiveObjects(int count) => SetActiveQuantityText($"Количество активных бомб: {count}");

    protected override void OnCountedCreateObjects(int count) => SetCreatedQuantityText($"Количество созданных бомб: {count}");

    protected override void OnCountedAllObjects(int count) => SetAllObjectsShowedQuantityText($"Количество показанных за все время бомб: {count}");
}
