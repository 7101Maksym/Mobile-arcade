using UnityEngine;

public class Boss1StartMovingScript : MonoBehaviour
{
    [SerializeField] private float _startSpeed = 5f;
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private GameObject _saveObject;

    private float _startDistance;
    [SerializeField] private bool _isMoving = false;
    private Vector2 _endPosition;

    private void Awake()
    {
        _endPosition = new Vector2(Camera.main.orthographicSize * Camera.main.aspect / 2f, 0);
        transform.position = _endPosition;
        _startDistance = Vector2.Distance(_targetObject.transform.position, _endPosition);
        transform.parent.position = new Vector2(Camera.main.orthographicSize * Camera.main.aspect * 3, 0);
    }

    private void FixedUpdate()
    {
        if (!_isMoving)
            return;

        float speed = (Vector2.Distance(_targetObject.transform.position, _endPosition) / _startDistance) * _startSpeed;

        _targetObject.transform.position = Vector2.MoveTowards(_targetObject.transform.position, _endPosition, speed * Time.fixedDeltaTime);

        if (Vector2.Distance(_targetObject.transform.position, _endPosition) <= 0.05f)
        {
            _saveObject.transform.parent = null;
            Destroy(this);
            Destroy(transform.parent.gameObject);
        }
    }

    public void StartMoving()
    {
        _isMoving = true;
    }
}
