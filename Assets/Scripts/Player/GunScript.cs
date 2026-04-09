using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CooldownEvent : UnityEvent<float> { }

public class GunScript : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private AudioClip _shootSound;

    public Sprite GunSprite;
    public CooldownEvent OnCooldown = new CooldownEvent();

    [Header("Shooting Settings")]
    [SerializeField] private float _bulletSpeed = 20f;
    [SerializeField] private float _fireRate = 2f;
    [SerializeField] private int _queueSize = 5;
    [SerializeField] private float _timePerShoot = 0.5f;

    private bool canShoot = true;

    public void StartShoot()
    {
        if (canShoot)
        {
            canShoot = false;
            
            for (int i = 1; i <= _queueSize; i++)
            {
                StartCoroutine(Shoot(i * _timePerShoot));
            }

            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        OnCooldown?.Invoke(_fireRate);

        yield return new WaitForSeconds(_fireRate);
        canShoot = true;
    }

    private IEnumerator Shoot(float time)
    {
        yield return new WaitForSeconds(time);

        GameObject bullet = Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = _firePoint.right * _bulletSpeed;

        if (GetComponent<AudioSource>() != null && _shootSound != null)
        {
            GetComponent<AudioSource>().PlayOneShot(_shootSound);
        }
    }

    private void OnDestroy()
    {
        OnCooldown.RemoveAllListeners();
    }
}
