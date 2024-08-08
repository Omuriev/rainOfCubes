using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    private Color _defaultColor;

    public void GetBomb(Vector3 position)
    {
        Bomb bomb = Pool.Get();
        bomb.transform.position = position;
    }

    protected override void GetAction(Bomb obj)
    {
        base.GetAction(obj);
        obj.Disappeared += OnDisappeared;
        _defaultColor = obj.GetComponent<Renderer>().material.color;
        ObjectCounter++;
        ChangeObjectCount(Pool.CountActive, Pool.CountAll, ObjectCounter);
    }

    protected override void OnDisappeared(Bomb bomb)
    {
        bomb.Disappeared -= OnDisappeared;
        Pool.Release(bomb);
        bomb.ChangeColor(_defaultColor);
        ChangeObjectCount(Pool.CountActive, Pool.CountAll, ObjectCounter);
    }
}
