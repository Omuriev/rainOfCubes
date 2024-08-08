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

    protected override void OnCountedActiveObjects(int count) => SetActiveQuantityText($"���������� �������� ����: {count}");

    protected override void OnCountedCreateObjects(int count) => SetCreatedQuantityText($"���������� ��������� ����: {count}");

    protected override void OnCountedAllObjects(int count) => SetAllObjectsShowedQuantityText($"���������� ���������� �� ��� ����� ����: {count}");
}
