using UnityEngine;

// Zastępuje stare Door.cs + DoorInteract.cs (był duplikat dwóch systemów na tych samych drzwiach).
// Sterowane wyłącznie przez InteractionController (raycast + E) - brak triggerów, brak "playerNearby" bugów.
[RequireComponent(typeof(Collider))]
public class Door : MonoBehaviour, IInteractable
{
    [Header("Obrót")]
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float speed = 3f;

    [Header("Stan")]
    [SerializeField] private bool startOpen = false;
    [SerializeField] private bool locked = false;

    [Header("UI")]
    [SerializeField] private string openPrompt = "Otwórz drzwi";
    [SerializeField] private string closePrompt = "Zamknij drzwi";
    [SerializeField] private string lockedPrompt = "Zamknięte";

    private bool _isOpen;
    private Quaternion _closedRotation;
    private Quaternion _openRotation;

    public bool CanInteract => !locked;
    public string InteractionPrompt => locked ? lockedPrompt : (_isOpen ? closePrompt : openPrompt);

    private void Start()
    {
        _closedRotation = transform.rotation;
        _openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, openAngle, 0f));
        _isOpen = startOpen;
        transform.rotation = _isOpen ? _openRotation : _closedRotation;
    }

    private void Update()
    {
        Quaternion target = _isOpen ? _openRotation : _closedRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * speed);
    }

    public void Interact(GameObject interactor)
    {
        if (locked) return;
        _isOpen = !_isOpen;
    }

    public void SetLocked(bool value) => locked = value;
}
