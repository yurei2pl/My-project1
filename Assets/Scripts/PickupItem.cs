using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        FlashlightController flashlight = other.GetComponentInChildren<FlashlightController>();
        if (flashlight != null)
            flashlight.canUse = true;

        gameObject.SetActive(false);
    }
}