using TMPro;
using UnityEngine;

public abstract class StatsUpdater<T> : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _createdQuantityText;
    [SerializeField] private ActiveObjectsStats _activeObjectsStats;

    protected int ActiveCubeQuantity = 0;
    protected int ActiveBombQuantity = 0;
    protected ActiveObjectsStats ShowAllActiveObjects => _activeObjectsStats;

    public void SetCreatedQuantityText(string text) => _createdQuantityText.text = text;

    protected abstract void OnCountedCreateObjects(int count);
    protected abstract void OnCountedActiveObjects(int count);
}
