using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    private Color _defaultColor;

    public void GetBomb(Vector3 position)
    {
        Bomb bomb = _pool.Get();
        bomb.transform.position = position;
    }

    protected override void ActionOnGet(Bomb obj)
    {
        base.ActionOnGet(obj);
        obj.Disappearing += OnDisappeared;
        _defaultColor = obj.GetComponent<Renderer>().material.color;
        ObjectCounter++;
        ChangeObjectCount(_pool.CountActive, _pool.CountAll);
    }

    protected override void OnDisappeared(Bomb bomb)
    {
        bomb.Disappearing -= OnDisappeared;
        _pool.Release(bomb);
        bomb.ChangeColor(_defaultColor);
        ChangeObjectCount(_pool.CountActive, _pool.CountAll);
    }
}
