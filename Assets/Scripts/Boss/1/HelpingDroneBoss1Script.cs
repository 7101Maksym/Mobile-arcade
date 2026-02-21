using UnityEngine;

public class HelpingDroneBoss1Script : MonoBehaviour
{
	[SerializeField] private float _speed = 2f;
	
	private GameObject _target;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = -transform.right * _speed;
    }

    private void FixedUpdate()
    {
        if (_target != null && _target.transform.position.x + 1 < transform.position.x)
        {
            Vector2 direction = (_target.transform.position - transform.position).normalized;
            direction.x = -1;
            _rb.velocity = direction * _speed;
        }
    }

    public void Initialize(GameObject target)
    {
        _target = target;
    }
}
