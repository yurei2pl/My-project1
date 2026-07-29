using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
    [SerializeField] private GameObject promptObject; // np. "Press E" UI/ikonka
    private bool inRange = false;
    private FlashlightController playerFlashlight;

    void Start()
    {
        if (promptObject != null) promptObject.SetActive(false);
    }

    void Update()
    {
        if (inRange && playerFlashlight != null && Input.GetKeyDown(KeyCode.E))
        {
            playerFlashlight.PickUpFlashlight();
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerFlashlight = other.GetComponentInParent<FlashlightController>();
            inRange = true;
            if (promptObject != null) promptObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            if (promptObject != null) promptObject.SetActive(false);
        }
    }
}