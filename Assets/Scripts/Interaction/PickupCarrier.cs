using UnityEngine;

// Podepnij na Player obok InteractionController.
// Trzyma aktualnie niesiony obiekt, obsługuje rzut i rotację (przytrzymaj R).
public class PickupCarrier : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Transform holdPos;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private int defaultLayer = 0;      // warstwa do której obiekt wraca po odłożeniu (0 = Default)
    [SerializeField] private string holdLayerName = "holdLayer";

    [Header("Wartości")]
    [SerializeField] private float throwForce = 8f;    // impulse, nie ciągła siła
    [SerializeField] private float rotationSensitivity = 3f;
    [SerializeField] private float holdSmoothing = 20f; // im wyżej, tym mniej "lagu" za kamerą

    [Header("Anty-clipping")]
    [SerializeField] private float clipCheckRadius = 0.2f;  // "grubość" trzymanego obiektu przy sprawdzaniu ścian
    [SerializeField] private LayerMask clipCheckMask = ~0;   // warstwy traktowane jako przeszkoda (wyklucz Player i holdLayer w Inspectorze)

    private GameObject _heldObj;
    private Rigidbody _heldRb;
    private Collider _heldCollider;
    private int _holdLayer;
    private bool _rotating;

    public bool IsCarrying => _heldObj != null;

    private void Awake()
    {
        if (playerCamera == null) playerCamera = Camera.main;
        _holdLayer = LayerMask.NameToLayer(holdLayerName);
    }

    private void Update()
    {
        if (_heldObj == null) return;

        HoldAtPosition();
        HandleRotation();
    }

    public void PickUp(GameObject obj)
    {
        if (_heldObj != null) return;
        if (!obj.TryGetComponent(out Rigidbody rb)) return;

        _heldObj = obj;
        _heldRb = rb;
        _heldCollider = obj.GetComponent<Collider>();

        _heldRb.isKinematic = true;
        _heldRb.useGravity = false;

        if (_heldCollider != null)
        {
            Collider playerCol = GetComponent<Collider>();
            if (playerCol != null) Physics.IgnoreCollision(_heldCollider, playerCol, true);
        }

        if (_holdLayer >= 0) _heldObj.layer = _holdLayer;
    }

    public void Drop()
    {
        if (_heldObj == null) return;
        ReleaseInternal();
    }

    public void Throw()
    {
        if (_heldObj == null) return;

        Rigidbody rb = _heldRb;
        Vector3 dir = playerCamera != null ? playerCamera.transform.forward : transform.forward;

        ReleaseInternal();

        rb.AddForce(dir * throwForce, ForceMode.VelocityChange);
    }

    private void ReleaseInternal()
    {
        if (_heldCollider != null)
        {
            Collider playerCol = GetComponent<Collider>();
            if (playerCol != null) Physics.IgnoreCollision(_heldCollider, playerCol, false);
        }

        _heldObj.layer = defaultLayer;
        _heldRb.isKinematic = false;
        _heldRb.useGravity = true;

        _heldObj = null;
        _heldRb = null;
        _heldCollider = null;
        _rotating = false;
    }

    private void HoldAtPosition()
    {
        if (_rotating || holdPos == null) return;

        Vector3 target = holdPos.position;

        // Anty-clipping: sprawdzamy czy między kamerą a punktem trzymania jest ściana.
        // Jeśli tak, przedmiot zatrzymuje się przed nią zamiast przenikać na drugą stronę.
        if (playerCamera != null)
        {
            Vector3 origin = playerCamera.transform.position;
            Vector3 toTarget = target - origin;
            float dist = toTarget.magnitude;

            if (dist > 0.001f)
            {
                Vector3 dir = toTarget / dist;
                if (Physics.SphereCast(origin, clipCheckRadius, dir, out RaycastHit hit, dist, clipCheckMask, QueryTriggerInteraction.Ignore))
                {
                    target = hit.point - dir * clipCheckRadius;
                }
            }
        }

        // Physics.MovePosition zamiast transform.position - nie przebija ścian tak łatwo
        // i nie psuje kolizji sąsiednich obiektów
        _heldRb.MovePosition(Vector3.Lerp(_heldObj.transform.position, target, holdSmoothing * Time.deltaTime));
    }

    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.R))
        {
            _rotating = true;

            float x = Input.GetAxis("Mouse X") * rotationSensitivity;
            float y = Input.GetAxis("Mouse Y") * rotationSensitivity;

            _heldObj.transform.Rotate(Vector3.up, -x, Space.World);
            _heldObj.transform.Rotate(Vector3.right, y, Space.World);
        }
        else
        {
            _rotating = false;
        }
    }
}
