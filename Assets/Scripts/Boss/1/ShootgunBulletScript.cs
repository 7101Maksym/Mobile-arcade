using UnityEngine;

public class ShootgunBulletScript : MonoBehaviour
{
	[SerializeField] private float _lifetime = 10f;
	[SerializeField] private float _lifetimeVariance = 5f;

    private void Awake()
	{
		Destroy(gameObject, _lifetime + UnityEngine.Random.Range(-_lifetimeVariance, _lifetimeVariance));
	}
}
