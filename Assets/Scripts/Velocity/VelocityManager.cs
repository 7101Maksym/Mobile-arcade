using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityManager : MonoBehaviour
{
    [SerializeField] private float _deltaVelocity = 0.1f;

    public float velocity = 5f;

    private void FixedUpdate()
    {
        if (velocity < 20)
        {
            velocity += _deltaVelocity * Time.fixedDeltaTime;
        }
    }
}
