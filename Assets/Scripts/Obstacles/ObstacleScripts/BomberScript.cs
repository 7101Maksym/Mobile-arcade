using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberScript : MonoBehaviour
{
    [SerializeField] private GameObject _bomb;
    [SerializeField] private Transform _point;

    private Rigidbody2D _rb;

    private float _time = 0.6f;
    private bool _isFinished = false;
    private float _velocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _velocity = FindObjectOfType<VelocityManager>().velocity;
    }

    private void Start()
    {
        StartCoroutine(DropBomb());

        _rb.position = new Vector2(_rb.position.x, 4.5f);
        _rb.AddForce(new Vector2(-(_velocity - 1), 0), ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (_isFinished)
        {
            StartCoroutine(DropBomb());

            _isFinished = false;
        }
    }

    private IEnumerator DropBomb()
    {
        yield return new WaitForSeconds(_time);

        Instantiate(_bomb, _point.position, new Quaternion(0, 0, 0, 0), _point);

        _time = 1.1f;

        _isFinished = true;
    }
}
