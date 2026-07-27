using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public bool canUse = false;
    [SerializeField] private GameObject flashlightLight; // referencja do światła/modelu

    private bool isOn = false;

    private void Update()
    {
        if (!canUse) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;
            flashlightLight.SetActive(isOn);
        }
    }
}