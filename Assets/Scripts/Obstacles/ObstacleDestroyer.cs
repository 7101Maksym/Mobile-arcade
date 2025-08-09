using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour
{
    [SerializeField] private float _backBorder;
    [SerializeField] private float _topBorder;
    [SerializeField] private float _bottomBorder;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _backBorder = Camera.main.ScreenToWorldPoint(Vector2.zero).x - _backBorder;
        _bottomBorder = Camera.main.ScreenToWorldPoint(Vector2.zero).y - _bottomBorder;
        _topBorder = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y + _topBorder;
    }

    private void FixedUpdate()
    {
        if (_rb.position.x < _backBorder || _rb.position.y > _topBorder || _rb.position.y < _bottomBorder)
        {
            Destroy(gameObject);
        }
    }
}
