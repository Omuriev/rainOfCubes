using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombStatsUpdater : StatsUpdater<BombSpawner>
{
    [SerializeField] private BombSpawner _bombSpawner;

    private void OnEnable()
    {
        _bombSpawner.CreateQuantityChanged += OnCountedCreateObjects;
        _bombSpawner.ActiveQuantityChanged += OnCountedActiveObjects;
    }

    private void OnDisable()
    {
        _bombSpawner.CreateQuantityChanged -= OnCountedCreateObjects;
        _bombSpawner.ActiveQuantityChanged -= OnCountedActiveObjects;
    }

    protected override void OnCountedActiveObjects(int count) => ShowAllActiveObjects.SetActiveBomb(count);

    protected override void OnCountedCreateObjects(int count) => SetCreatedQuantityText("Количество созданных бомб за все время " + count.ToString());
}
