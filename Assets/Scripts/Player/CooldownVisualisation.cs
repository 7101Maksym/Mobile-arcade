using UnityEngine;
using UnityEngine.UI;

public class CooldownVisualisation : MonoBehaviour
{
    private Slider _slider;
    private bool _isCooldown = false;

    private void FixedUpdate()
    {
        if (_isCooldown)
        {
            _slider.value -= Time.fixedDeltaTime;
            if (_slider.value <= 0)
            {
                _isCooldown = false;
                _slider.value = 0;
            }
        }
    }

    public void StartCooldown(float cooldown)
    {
        if (_slider == null)
        {
            _slider = GetComponent<Slider>();
        }
        _slider.maxValue = cooldown;
        _slider.value = cooldown;
        _isCooldown = true;
    }
}
