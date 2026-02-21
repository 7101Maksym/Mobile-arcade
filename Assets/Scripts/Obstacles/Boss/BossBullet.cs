using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public Vector2 Direction;

    [SerializeField] private float _speed = 1f;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rb.AddForce(Direction * -_speed, ForceMode2D.Impulse);
    }
}
