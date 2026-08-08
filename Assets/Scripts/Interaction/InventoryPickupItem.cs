using UnityEngine;
using UnityEngine.Events;

// Zastępuje FlashlightPickup/NotePickup - jeden skrypt, w Inspectorze podpinasz co ma się stać.
// Przykład: OnPickedUp -> FlashlightController.PickUpFlashlight()
//           OnPickedUp -> NoteViewer.ShowNote(this prefab, tekst) [przez wrapper]
public class InventoryPickupItem : MonoBehaviour, IInteractable
{
    [System.Serializable]
    public class GameObjectEvent : UnityEvent<GameObject> { }

    [SerializeField] private string prompt = "Podnieś";
    [SerializeField] private bool disableOnPickup = true;
    [SerializeField] private GameObjectEvent onPickedUp; // parametr = interactor (gracz)

    public string InteractionPrompt => prompt;
    public bool CanInteract => true;

    public void Interact(GameObject interactor)
    {
        onPickedUp?.Invoke(interactor);
        if (disableOnPickup) gameObject.SetActive(false);
    }
}
