using System;
using TMPro;
using UnityEngine;

public abstract class StatsUpdater<T> : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _createdQuantityText;
    [SerializeField] private TextMeshProUGUI _activeQuantityText;
    [SerializeField] private TextMeshProUGUI _allObjectsShowedQuantityText;

    protected abstract void OnCountedCreateObjects(int count);
    protected abstract void OnCountedActiveObjects(int count);
    protected abstract void OnCountedAllObjects(int count);

    protected void SetCreatedQuantityText(string text) => _createdQuantityText.text = text;
    protected void SetActiveQuantityText(string text) => _activeQuantityText.text = text;
    protected void SetAllObjectsShowedQuantityText(string text) => _allObjectsShowedQuantityText.text = text;
}
