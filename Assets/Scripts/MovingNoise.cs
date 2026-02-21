using UnityEngine;

public class MovingNoise : MonoBehaviour
{
    [SerializeField] private float _borderH = 1;
    [SerializeField] private float _borderV = 1;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private bool _useAutoStart = true;

    private float noise;
    private bool _isMoving = false;
    private Vector2 _startPosition = Vector2.zero;

    private void Awake()
    {
        if (_useAutoStart)
            StartMoving();
    }

    private void FixedUpdate()
    {
        if (!_isMoving)
            return;

        noise += Time.fixedDeltaTime * _speed;

        float x = Mathf.Lerp(-_borderH, _borderH, Mathf.PerlinNoise(noise, 0));
        float y = Mathf.Lerp(-_borderV, _borderV, Mathf.PerlinNoise(0, noise));

        transform.position = new Vector3(_startPosition.x + x, _startPosition.y + y, transform.position.z);

        if (transform.parent != null)
            _startPosition = transform.parent.position;
    }

    public void StartMoving()
    {
        if (_isMoving)
            return;

        _isMoving = true;
    }

    public void StopMoving()
    {
        _isMoving = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (_startPosition == Vector2.zero)
            _startPosition = transform.position;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(_startPosition, new Vector3(_borderH * 2, _borderV * 2, 0));
        Gizmos.DrawSphere(gameObject.transform.position, 0.1f);
    }
}
