using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstacles;
    [SerializeField] private Transform _capacitor;

    [Header("Neded velocity")]
    [SerializeField] private float _velocity;
    [Header("Max velocirty")]
    [SerializeField] private float _maxVelocity = 21f;
    [SerializeField] private float _generateInterval = 1f;

    private GameObject _obstacle;
    private VelocityManager _manager;

    private bool _canGenerate = true;
    private float _interval;
    

    private void Awake()
    {
        _manager = FindObjectOfType<VelocityManager>();
    }

    private void FixedUpdate()
    {
        if (_canGenerate && _velocity <= _manager.velocity && _manager.velocity <= _maxVelocity)
        {
            _canGenerate = false;

            _obstacle = _obstacles[Random.Range(0, _obstacles.Length - 1)];
            _obstacle.transform.position = new Vector2(20, 0);

            Instantiate(_obstacle, _capacitor);

            _interval = UnityEngine.Random.Range(_generateInterval - _generateInterval, _generateInterval + _generateInterval);

            StartCoroutine(Generate());
        }
    }

    private IEnumerator Generate()
    {
        yield return new WaitForSeconds(_interval);

        _canGenerate = true;
    }
}
