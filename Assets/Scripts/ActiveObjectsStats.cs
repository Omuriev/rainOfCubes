using TMPro;
using UnityEngine;

public class ActiveObjectsStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _allActiveObjectsQuantityText;

    private int _allObjectQuantity = 0;
    private int _activeBomb;
    private int _activeCube;

    public void SetActiveBomb(int activeBomb)
    {
        _activeBomb = activeBomb;
        ChangeActiveText();
    }

    public void SetActiveCube(int activeCube)
    {
        _activeCube = activeCube;
        ChangeActiveText();
    }

    private void ChangeActiveText()
    {
        _allObjectQuantity = _activeBomb + _activeCube;
        _allActiveObjectsQuantityText.text = "Общее количество активных объектов: " + _allObjectQuantity.ToString();
    }
}
