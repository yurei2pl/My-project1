using UnityEngine;

// Podpięte pod InteractionController (raycast + E) - brak własnej logiki triggera/zasięgu.
public class FlashlightPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt = "Podnieś latarkę";

    public string InteractionPrompt => prompt;
    public bool CanInteract => true;

    public void Interact(GameObject interactor)
    {
        FlashlightController controller = interactor.GetComponentInChildren<FlashlightController>();
        if (controller == null) return;

        controller.PickUpFlashlight();
        gameObject.SetActive(false);
    }
}
