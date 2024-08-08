using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private List<Color> _colors = new List<Color>();

    private Renderer _meshRenderer;
    private Color _currentColor;
    private bool _isTouched = false;
    private float _lifeTime;
    private float _minLifeTimeThreshold = 2;
    private float _maxLifeTimeThreshold = 5;
    private Coroutine _waitBeforeDisappearingCoroutine;

    public event Action<Cube> Disappeared;

    public Color StartColor => _currentColor;
    public Renderer MeshRenderer => _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<Renderer>();
        _currentColor = _meshRenderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isTouched == false)
        {
            if (collision.gameObject.TryGetComponent(out Platform platform))
            {
                if (_waitBeforeDisappearingCoroutine != null)
                {
                    StopCoroutine(_waitBeforeDisappearingCoroutine);
                }

                _isTouched = true;
                _lifeTime = Utils.GetLifeTime(_minLifeTimeThreshold, _maxLifeTimeThreshold);
                _meshRenderer.material.color = GetColor();
                _waitBeforeDisappearingCoroutine = StartCoroutine(WaitBeforeDisappearing());
            }
        }
    }

    private IEnumerator WaitBeforeDisappearing()
    {
        WaitForSeconds waitTime = new WaitForSeconds(_lifeTime);

        yield return waitTime;

        _isTouched = false;
        Disappeared?.Invoke(this);
    }

    private Color GetColor() => _colors[UnityEngine.Random.Range(0, _colors.Count - 1)];
}
