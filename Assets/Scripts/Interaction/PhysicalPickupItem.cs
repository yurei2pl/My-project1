using UnityEngine;

// Wrzuć na każdy obiekt który gracz ma fizycznie nosić/rzucać (wymaga Rigidbody + Collider).
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PhysicalPickupItem : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt = "Podnieś";

    public string InteractionPrompt => prompt;
    public bool CanInteract => true;

    public void Interact(GameObject interactor)
    {
        PickupCarrier carrier = interactor.GetComponent<PickupCarrier>();
        if (carrier == null || carrier.IsCarrying) return;

        carrier.PickUp(gameObject);
    }
}
