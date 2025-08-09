using UnityEngine;

public class BackgroroundImage : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private VelocityManager _manager;
    private Rigidbody2D _rb;

    public Vector2 Size;
    public float Scale;
    public float Factor;

    private void Awake()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _manager = FindObjectOfType<VelocityManager>();
        _rb = GetComponent<Rigidbody2D>();

        Size = _sprite.sprite.bounds.size;
        Scale = Camera.main.orthographicSize * 2f / Size.y;

        _sprite.transform.localScale *= Scale;
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2((-_manager.velocity) + Factor, 0);
    }

    private void Update()
    {
        if ((Size.x / 2 * _sprite.transform.localScale.x) + _sprite.transform.position.x < -(Camera.main.orthographicSize * Camera.main.aspect))
        {
            Destroy(gameObject);
        }
    }
}
