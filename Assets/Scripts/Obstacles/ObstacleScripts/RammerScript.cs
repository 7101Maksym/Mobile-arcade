using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RammerScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Transform _position;

    private float _velocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _position = GameObject.Find("Player").transform;
        _velocity = FindObjectOfType<VelocityManager>().velocity;
    }

    private void Start()
    {
        _rb.position = new Vector2(_rb.position.x, _position.position.y);
        _rb.AddForce(new Vector2(-(_velocity + 6), 0), ForceMode2D.Impulse);
    }
}
