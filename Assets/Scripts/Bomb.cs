using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour 
{
    [SerializeField] private float _explosionRadius = 10f;
    [SerializeField] private float _explosionForce = 10f;

    private float _minLifeTimeThreshold = 2;
    private float _maxLifeTimeThreshold = 5;

    private Coroutine _waitBeforeExplosionCoroutine;
    private Renderer _renderer;

    public event Action<Bomb> Disappeared;

    private void Awake() => _renderer = GetComponent<Renderer>();

    private void OnEnable()
    {
        if (_waitBeforeExplosionCoroutine != null)
        {
            StopCoroutine(_waitBeforeExplosionCoroutine);
        }

        _waitBeforeExplosionCoroutine = StartCoroutine(WaitBeforeExplosion());
    }

    public void ChangeColor(Color color) => _renderer.material.color = color;

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Rigidbody rigidbody))
                rigidbodies.Add(rigidbody);
        }

        return rigidbodies;
    }

    private void Explode(List<Rigidbody> rigidbodies)
    {
        for (int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    private IEnumerator WaitBeforeExplosion()
    {
        float waitTime = 1f;
        float lifeTime = Utils.GetLifeTime(_minLifeTimeThreshold, _maxLifeTimeThreshold);
        float timer = 0f;

        while (timer <= lifeTime)
        {
            Color color = _renderer.material.color;
            color.a = Mathf.Lerp(color.a, 0, timer / lifeTime);
            _renderer.material.color = color;
            timer++;

            yield return new WaitForSeconds(waitTime);
        }

        Explode(GetExplodableObjects());
        Disappeared?.Invoke(this);
    }
}
