using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDrone : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _point;

    private Rigidbody2D _rb;

    private float _velocity;
    private bool _shoot = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _velocity = FindObjectOfType<VelocityManager>().velocity;
    }

    private void Start()
    {
        Vector2 newPos = Vector2.zero;

        newPos.y = UnityEngine.Random.Range(0, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        newPos.x += 15;
        _rb.position = newPos;

        _rb.AddForce(new Vector2(-UnityEngine.Random.Range(_velocity - 1, _velocity + 1), UnityEngine.Random.Range(-0.5f, 0.5f)), ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (_shoot)
        {
            StartCoroutine(Shoot());
            _shoot = false;
        }
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.3f);

        Instantiate(_bullet, _point.position, new Quaternion(0, 0, 0, 0), _point);

        _shoot = true;
    }
}
