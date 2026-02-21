using System.Collections;
using UnityEngine;

public class TurretScriptBoss1 : MonoBehaviour
{
	public Transform target;

	public float MaxHealths = 100f;
	public float Healths;

	[SerializeField] private GameObject _bullet;
	[SerializeField] private Transform _generatingPoint;
	[SerializeField] private Transform _barrel;

	[Header("Shooting Settings")]
	[SerializeField] private float _shootingFrequency = 0.1f;
	[SerializeField] private float _shootingDelay = 3f;
	[Min(1)]
	[SerializeField] private int _bulletsPerShot = 1;
	[SerializeField] private float _startDelay = 0;

	[Header("Grafic objects")]
	[SerializeField] private SpriteRenderer _barrelSprite;
	[SerializeField] private SpriteRenderer _corpusSprite;
	[SerializeField] private SpriteRenderer _destroyedSprite;

	[Header("Rotating limit")]
	[SerializeField] private float _border_1 = 60f;
	[SerializeField] private float _border_2 = -60f;

	private float _neededRotation;
	private bool _notShoot = false;

	public bool log = false;

	private void Awake()
	{
		Healths = MaxHealths;
		StartCoroutine(Shooting(true));
	}

	private void FixedUpdate()
	{
		if (Healths > 0)
		{
			_neededRotation = GetRightAngle(target.position - transform.position, _barrel);

			float currentAngle = _barrel.localEulerAngles.z;
			if (currentAngle > 180f) currentAngle -= 360f;

			if (currentAngle < _border_2)
			{
				_barrel.localRotation = Quaternion.Euler(0f, 0f, _border_2 + 1f);
			}
			else if (currentAngle > _border_1)
			{
				_barrel.localRotation = Quaternion.Euler(0f, 0f, _border_1 - 1f);
			}
			else
			{
				if (_neededRotation > 1)
					_barrel.Rotate(0, 0, -1f);
				else if (_neededRotation < -1f)
					_barrel.Rotate(0, 0, 1f);
			}

            if (_notShoot)
            {
                _notShoot = false;
                StartCoroutine(Shooting(false));
            }
        }
		else
		{
			_barrelSprite.enabled = false;
			_corpusSprite.enabled = false;
			_destroyedSprite.enabled = true;
		}
	}

	private IEnumerator Shooting(bool useStartDelay)
	{
		if (useStartDelay)
		{
			yield return new WaitForSeconds(_startDelay);
		}

		yield return new WaitForSeconds(_shootingDelay);

		for (int i = 0; i < _bulletsPerShot; i++)
		{

			_bullet.GetComponent<BossBullet>().Direction = -_barrel.up + _barrel.right * UnityEngine.Random.Range(-0.05f, 0.05f);
			Instantiate(_bullet, _generatingPoint.position, _generatingPoint.rotation);

			yield return new WaitForSeconds(_shootingFrequency);
		}

		_notShoot = true;
	}

	private float GetRightAngle(Vector2 direct, Transform rotatingObject)
	{
		float angle = Vector2.Angle(direct, rotatingObject.up);
		float controlAnglel = Vector2.Angle(direct, rotatingObject.right);

		if (controlAnglel > 90)
		{
			return -angle;
		}
		else
		{
			return angle;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + (Vector3)GetDirection(_border_1) * 3);
		Gizmos.DrawLine(transform.position, transform.position + (Vector3)GetDirection(_border_2) * 3);
        Gizmos.DrawLine(transform.position, target.position);
    }

	private Vector2 GetDirection(float angle)
	{
		float angleRad = (angle - 180) * Mathf.Deg2Rad;
		return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;
	}
}
