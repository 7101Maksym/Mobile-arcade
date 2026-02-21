using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    private Rigidbody2D _rb;

    private float _velocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _velocity = FindObjectOfType<VelocityManager>().velocity;
    }

    private void Start()
    {
        _rb.AddForce(new Vector2(-(_velocity + _speed), 0), ForceMode2D.Impulse);
    }
}
