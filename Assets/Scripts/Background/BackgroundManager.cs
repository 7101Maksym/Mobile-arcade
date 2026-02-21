using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
	[SerializeField] private GameObject _image;
	[SerializeField] private float _parallaxFactor = 0f;
	[SerializeField] private int _orderInLayer = 0;
	[SerializeField] private Sprite _sprite;

	private GameObject _lastImage;
	private BackgroroundImage _lastBack;

	private void Update()
	{
		if (_lastBack != null)
		{
			if (_lastImage.transform.position.x + (_lastBack.Size.x * _lastBack.Scale) / 2 < Camera.main.orthographicSize * Camera.main.aspect)
			{
				Vector2 pos = Vector2.zero;

				pos.x = _lastImage.transform.position.x + _lastBack.Size.x * _lastBack.Scale;

                _image.GetComponentInChildren<SpriteRenderer>().sprite = _sprite;
                _image.GetComponentInChildren<SpriteRenderer>().sortingOrder = _orderInLayer;

                _lastImage = Instantiate(_image, pos, new Quaternion(0, 0, 0, 0), transform);

				_lastBack = _lastImage.GetComponent<BackgroroundImage>();
                _lastBack.Factor = _parallaxFactor;
            }
		}
		else
		{
            Vector2 pos = Vector2.zero;

			pos.x = -(Camera.main.orthographicSize * Camera.main.aspect * 2f);
			pos.x += _sprite.bounds.size.x * (Camera.main.orthographicSize * 2f / (_sprite.bounds.size.y * 2));

            _image.GetComponentInChildren<SpriteRenderer>().sprite = _sprite;
            _image.GetComponentInChildren<SpriteRenderer>().sortingOrder = _orderInLayer;

            _lastImage = Instantiate(_image, pos, new Quaternion(0, 0, 0, 0), transform);

            _lastBack = _lastImage.GetComponent <BackgroroundImage>();
			_lastBack.Factor = _parallaxFactor;
        }
	}
}
