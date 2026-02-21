using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ShootGunScript : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Transform _barrelSprite;
    [Header("Shoot Settings")]
    [SerializeField] private float _shootInterval = 10f;
    [SerializeField] private int _bulletCount = 5;
    [SerializeField] private float _shootForceMin = 5f;
    [SerializeField] private float _shootForceMax = 10f;
    [Header("Recoil Settings")]
    [SerializeField] private Transform _normalPoint;
    [SerializeField] private Transform _recoilPoint;
    [SerializeField] private float _recoilSpeed = 10f;

    private bool _isShooting = true;

    private void Awake()
    {
        StartCoroutine(Shoot(5));
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(_barrelSprite.position, _normalPoint.position) > 0.01f)
        {
            _barrelSprite.position = Vector2.MoveTowards(_barrelSprite.position, _normalPoint.position, _recoilSpeed * Time.fixedDeltaTime);
        }

        if (!_isShooting)
        {
            _isShooting = true;
            StartCoroutine(Shoot(_shootInterval));
        }
    }

    public IEnumerator Shoot(float delay)
    {
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < _bulletCount; i++)
        {
            GameObject bullet = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            rb.AddForce(_shootPoint.right * -UnityEngine.Random.Range(_shootForceMin, _shootForceMax), ForceMode2D.Impulse);
            rb.AddForce(new Vector2(0, UnityEngine.Random.Range(-10f, 10f)), ForceMode2D.Impulse);
        }

        _barrelSprite.position = _recoilPoint.position;

        _isShooting = false;
    }
}
