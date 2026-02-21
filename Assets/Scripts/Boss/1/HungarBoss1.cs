using System.Collections;
using UnityEngine;

public class Hungar : MonoBehaviour
{
	[SerializeField] private Transform _pointClosed;
	[SerializeField] private Transform _pointOpened;
	[SerializeField] private GameObject _drone;
	[SerializeField] private GameObject _target;
	[SerializeField] private Transform _emitingPoint;

	private bool _emited = false, _opening = false, _canOpen = true;

	public void OpenHungar()
	{
        _opening = true;
    }

	private void CloseHungar()
	{
        _opening = false;
    }

	private void FixedUpdate()
	{
		if (Vector2.Distance(transform.position, _pointOpened.position) > 0.01f && _opening)
		{
			gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, _pointOpened.position, 0.5f * Time.fixedDeltaTime);
		}
		else if (Vector2.Distance(transform.position, _pointOpened.position) <= 0.01f && _opening)
		{
			if (!_emited)
			{
				StartCoroutine(EmitDrones());
			}
		}

        if (Vector2.Distance(transform.position, _pointClosed.position) > 0.01f && !_opening)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, _pointClosed.position, 0.5f * Time.fixedDeltaTime);
        }

		if (_canOpen)
		{
            StartCoroutine(StartIteration());
            _canOpen = false;
        }
    }

	private IEnumerator EmitDrones()
	{
        _emited = true;

        for (int i = 0; i < 3; i++)
		{
			GameObject drone = Instantiate(_drone, _emitingPoint.position, Quaternion.identity);
			drone.GetComponent<HelpingDroneBoss1Script>().Initialize(_target);
			yield return new WaitForSeconds(1f);
		}

        yield return new WaitForSeconds(1f);

		CloseHungar();

		yield return new WaitForSeconds(5f);

        _emited = false;
        _canOpen = true;
    }

	private IEnumerator StartIteration()
	{
		yield return new WaitForSeconds(10f);
        OpenHungar();
    }
}
