using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoysticScript : MonoBehaviour
{
    [SerializeField] private Moving _mover;
    [SerializeField] private float _radius;
    [SerializeField] private Image _button;
    [SerializeField] private Image _field;
    [SerializeField] private Canvas _canvas;

    [Header("Idents")]
    [SerializeField] private float _identH;
    [SerializeField] private float _identV;

    private Vector3 _point, _fieldPos;

    

    private void Awake()
    {
        _fieldPos.x = _canvas.renderingDisplaySize.x - _identH;
        _fieldPos.y = _identV;

        _field.transform.position = _fieldPos;
    }

    private void Update()
    {
        if (Input.touchCount != 0)
        {
            _point = Input.GetTouch(0).position;
        }
        else
        {
            _point = _field.transform.position;
        }

        if (Vector2.Distance(_point, _field.transform.position) <= _radius)
        {
            _button.transform.position = _point;
        }
        else
        {
            _button.transform.position = _field.transform.position + (_point - _field.transform.position).normalized * 200;
        }
    }

    private void FixedUpdate()
    {
        _mover.Velocity = (_button.transform.position - _field.transform.position).normalized;
        
        if (Vector2.Distance(_button.transform.position, _field.transform.position) > _radius / 10)
        {
            _mover.Speed = _mover.MaxSpeed;
        }
        else
        {
            _mover.Speed = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        _fieldPos.x = _canvas.renderingDisplaySize.x - _identH;
        _fieldPos.y = _identV;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_field.transform.position, _radius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_fieldPos, _radius / 4);
    }
}
