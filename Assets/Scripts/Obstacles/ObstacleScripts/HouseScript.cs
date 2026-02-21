using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScript : MonoBehaviour
{
    [SerializeField] private Transform _border;

    private Rigidbody2D _rb;

    private float _velocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _velocity = FindObjectOfType<VelocityManager>().velocity;
    }

    private void Start()
    {
        float deltaY = Mathf.Abs(_border.position.y);
        Vector2 cameraAngle = Camera.main.ScreenToWorldPoint(new Vector2(Screen.height, 0));
        cameraAngle.x += 15;
        cameraAngle.y += deltaY;
        transform.position = cameraAngle;
        _rb.AddForce(new Vector2(-_velocity, 0), ForceMode2D.Impulse);
    }
}
