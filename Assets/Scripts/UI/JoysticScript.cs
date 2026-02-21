using UnityEngine;
using UnityEngine.UI;

public class JoysticScript : MonoBehaviour
{
    [SerializeField] private Moving _mover;
    [SerializeField] private float _radius;
    [SerializeField] private Image _button;
    [SerializeField] private Image _field;
    [SerializeField] private Canvas _canvas;

    private Vector3 _point;
    private Vector2 _firstPoint = Vector2.zero;

    private void Update()
    {
        if (Input.touchCount != 0)
        {
            _point = Input.GetTouch(0).position;

            if (_firstPoint == Vector2.zero)
            {
                _firstPoint = _point;
                _field.transform.position = _firstPoint;
                _field.enabled = true;
                _button.enabled = true;
            }
        }
        else
        {
            _point = _field.transform.position;
            _firstPoint = Vector2.zero;
            _field.enabled = false;
            _button.enabled = false;
        }

        if (Vector2.Distance(_point, _field.transform.position) <= _radius)
        {
            _button.transform.position = _point;
        }
        else
        {
            _button.transform.position = _field.transform.position + (_point - _field.transform.position).normalized * _radius;
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_field.transform.position, _radius);
    }
}
