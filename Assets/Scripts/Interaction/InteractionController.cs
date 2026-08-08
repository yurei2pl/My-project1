using UnityEngine;
using TMPro;

// Podepnij na Player. Wymaga że kamera jest przypisana (StarterAssets FPS -> Camera.main).
// Jeden raycast na klatkę, jeden punkt wejścia dla drzwi/pickupów/notatek.
public class InteractionController : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] private Camera playerCamera;      // jeśli puste, użyje Camera.main
    [SerializeField] private float interactRange = 3.5f;
    [SerializeField] private LayerMask interactMask = ~0; // domyślnie wszystkie warstwy

    [Header("UI (opcjonalne)")]
    [SerializeField] private GameObject promptRoot;    // panel z tekstem "Naciśnij E..."
    [SerializeField] private TMP_Text promptText;

    [Header("Fizyczne trzymanie")]
    [SerializeField] private PickupCarrier carrier;     // jeśli puste, spróbuje GetComponent na sobie

    private IInteractable _current;

    private void Awake()
    {
        if (playerCamera == null) playerCamera = Camera.main;
        if (carrier == null) carrier = GetComponent<PickupCarrier>();
        if (promptRoot != null) promptRoot.SetActive(false);
    }

    private void Update()
    {
        // Gdy gracz coś fizycznie niesie, E = odłóż, nie skanujemy dalej
        if (carrier != null && carrier.IsCarrying)
        {
            HidePrompt();
            if (Input.GetKeyDown(KeyCode.E))
            {
                carrier.Drop();
            }
            if (Input.GetMouseButtonDown(0))
            {
                carrier.Throw();
            }
            return;
        }

        ScanForInteractable();

        if (_current != null && Input.GetKeyDown(KeyCode.E) && _current.CanInteract)
        {
            _current.Interact(gameObject);
        }
    }

    private void ScanForInteractable()
    {
        if (playerCamera == null)
        {
            _current = null;
            HidePrompt();
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactMask, QueryTriggerInteraction.Collide))
        {
            IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
                _current = interactable;
                ShowPrompt(interactable.InteractionPrompt);
                return;
            }
        }

        _current = null;
        HidePrompt();
    }

    private void ShowPrompt(string text)
    {
        if (promptRoot != null) promptRoot.SetActive(true);
        if (promptText != null) promptText.text = text;
    }

    private void HidePrompt()
    {
        if (promptRoot != null) promptRoot.SetActive(false);
    }
}
