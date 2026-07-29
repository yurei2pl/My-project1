using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private Light flashlightLight;
    [SerializeField] private GameObject flashlightModel;
    private bool hasFlashlight = false;
    private bool isOn = false;

    void Start()
    {
        if (flashlightLight != null) flashlightLight.enabled = false;
        if (flashlightModel != null) flashlightModel.SetActive(false);
    }

    void Update()
    {
        if (hasFlashlight && Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;
            flashlightLight.enabled = isOn;
            if (flashlightModel != null) flashlightModel.SetActive(isOn);
        }
    }

    public void PickUpFlashlight()
    {
        hasFlashlight = true;
    }
}