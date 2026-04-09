using UnityEngine;
using UnityEngine.UI;

public class GunManager : MonoBehaviour
{
    [SerializeField] private GameObject _shootButton;

    private GunScript[] _guns;
    private GameObject _buttons;

    private void Start()
    {
        //while (transform.childCount > 0)
        //{
        //    Destroy(transform.GetChild(0).gameObject);
        //}

        _guns = GetComponentsInChildren<GunScript>();
        _buttons = GameObject.FindGameObjectWithTag("Buttons");

        for (int i = 0; i < _guns.Length; i++)
        {
            GameObject button = Instantiate(_shootButton, _buttons.transform);

            Image[] images = button.GetComponentsInChildren<Image>();

            for (int j = 0; j < images.Length; j++)
            {
                if (images[j].gameObject.CompareTag("GunImage"))
                {
                    images[j].sprite = _guns[i].GunSprite;
                }
            }

            button.GetComponent<Button>().onClick.AddListener(_guns[i].StartShoot);

            CooldownVisualisation _cooldown = button.GetComponentInChildren<CooldownVisualisation>();
            _guns[i].OnCooldown.AddListener(_cooldown.StartCooldown);
        }
    }
}
