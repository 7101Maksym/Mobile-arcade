using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class HealthsScript : MonoBehaviour
{
	[Description("Root of object, which will be destroyed after death. If it NULL, object will be destroyed itself. Use for objects with many colliders, like tanks and houses.")]
	[SerializeField] private GameObject _root;
	[Description("If it TRUE, object can't take damage.")]
	[SerializeField] private bool _godMode = false;
	[Description("If it FALSE, object cann't take damage. Use for bullets and static obstacles(houses).")]
	[SerializeField] private bool _damageble = false;
	[Description("If it TRUE, object will be destroyed after any colision.")]
	[SerializeField] private bool _bullet = false;
	[SerializeField] private float _damage = 10f;
	public bool Damageble { get; private set; }
	[Description("Explosion animation, which will be played when object will be destroyed.")]
	[SerializeField] private AnimationClip _explosionAnimation = null;
	[SerializeField] private float _explosionAnimationScale = 1f;
	[SerializeField] private Color _explosionColor = Color.white;
    [SerializeField] private SpriteRenderer _renderer;
	[Header("Healths")]
	[SerializeField] private float _currentHealths = 20f;
	[SerializeField] private float _maxHealths = 20f; //if in game will be some healths recoverers, this variable will be useful for them
	[SerializeField] private bool _hasShield = false;
	[SerializeField] private float _currentShield = 10f;
	[SerializeField] private float _maxShield = 10f;
	[Description("Speed of shield recovery in points per second.")]
	[SerializeField] private float _recoveringShield = 1f;

	private GameObject _explosionEffect;

	private void Awake()
	{
		Damageble = _damageble;
		_explosionEffect = Resources.Load<GameObject>("ExplosionEffect");

		if (_explosionEffect == null)
		{
			Debug.LogWarning("Explosion effect not found in Resources folder.");
		}

#if !UNITY_EDITOR
		_godMode = false;
#endif
	}

	private void FixedUpdate()
	{
		if (_hasShield && _currentShield < _maxShield)
		{
			_currentShield += _recoveringShield * Time.fixedDeltaTime;

			if (_currentShield > _maxShield)
			{
				_currentShield = _maxShield;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponentInChildren<HealthsScript>() != null)
		{
			HealthsScript otherHealths = collision.gameObject.GetComponentInChildren<HealthsScript>();

			if (otherHealths.Damageble)
			{
				otherHealths.TakeDamage(_damage);
			}
		}

		if (_bullet)
		{
			Die();
		}
	}

	public void TakeDamage(float damage)
	{
		Debug.Log($"{gameObject.name} took {damage} damage.");

		if (_godMode)
		{
			Debug.Log($"{gameObject.name} is in god mode and cannot take damage.");
			return;
		}

		_currentShield -= damage;

		if (_currentShield < 0)
		{
			_currentHealths += _currentShield;
			_currentShield = 0;
		}

		if (_currentHealths <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		if (_explosionEffect != null && _explosionAnimation != null)
		{
			GameObject explosion = Instantiate(_explosionEffect, transform.position, Quaternion.identity);

			Vector2 scale = _renderer != null ? (Vector2)_renderer.bounds.size / explosion.GetComponent<SpriteRenderer>().bounds.size : new Vector2(1, 1);
            Vector2 explosionScale = new Vector2(Mathf.Max(scale.x, scale.y), Mathf.Max(scale.x, scale.y));
            explosionScale *= _explosionAnimationScale;
            explosion.transform.localScale = explosionScale;

            explosion.GetComponent<ExplosionEffect>().StartExplosion(_explosionAnimation, _explosionColor);
		}
		else
		{
			Debug.LogWarning("Explosion effect or animation is not set. Object will be destroyed without explosion.");
		}

		if (_root != null)
		{
			Destroy(_root);
			return;
		}

		Debug.LogWarning("Root object is not set. Destroying the object itself.");
		Destroy(gameObject);
	}
}
