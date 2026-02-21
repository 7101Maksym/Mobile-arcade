using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class VelocityManager : MonoBehaviour
{
    [SerializeField] private float _deltaVelocity = 0.1f;
    [SerializeField] private float _stoppingSpeed = 2;
    [SerializeField] private float _maxVelocity = 20f;
    [SerializeField] private float _timeOnMaxVelocity = 10f;

    public UnityEvent BossFight;

    public float velocity = 5f;

    private bool _stopping = false, _delayStarted = false;

    public IEnumerator StopMoving()
    {
        _delayStarted = true;

        yield return new WaitForSeconds(_timeOnMaxVelocity);

        _stopping = true;
        _deltaVelocity = -_deltaVelocity * _stoppingSpeed;
        BossFight?.Invoke();
    }

    private void FixedUpdate()
    {
        if (velocity < _maxVelocity && velocity > 0 && !_stopping)
        {
            velocity += _deltaVelocity * Time.fixedDeltaTime;
        }

        if (velocity <= 0 )
        {
            velocity = 0;
        }
        else if (_stopping)
        {
            velocity += _deltaVelocity * Time.fixedDeltaTime;
        }

        if (velocity > _maxVelocity && !_delayStarted)
        {
            StartCoroutine(StopMoving());
        }
    }
}
