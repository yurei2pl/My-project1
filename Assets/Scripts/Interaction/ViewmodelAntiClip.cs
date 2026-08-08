using UnityEngine;

// Wrzuć na model widoczny w ręku (dziecko kamery, np. latarka) - odsuwa go od kamery
// gdy jest wolna przestrzeń, i podciąga bliżej gdy przed graczem jest ściana,
// żeby model nie przenikał przez geometrię.
public class ViewmodelAntiClip : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask wallMask = ~0; // wyklucz Player i holdLayer w Inspectorze
    [SerializeField] private float normalDistance = 0.5f;  // domyślny dystans modelu od kamery
    [SerializeField] private float minDistance = 0.15f;    // jak blisko kamery może podjechać przy ścianie
    [SerializeField] private float smoothing = 15f;

    private Vector3 _localDirection; // kierunek w lokalnej przestrzeni kamery, ustalony raz na starcie
    private float _currentDistance;

    private void Start()
    {
        if (playerCamera == null) playerCamera = GetComponentInParent<Camera>();
        if (playerCamera == null) playerCamera = Camera.main;

        _currentDistance = normalDistance;

        // zapamiętaj początkowy kierunek lokalny (żeby zachować offset X/Y ustawiony w edytorze)
        _localDirection = transform.localPosition.normalized;
        if (_localDirection == Vector3.zero) _localDirection = Vector3.forward;
    }

    private void LateUpdate()
    {
        if (playerCamera == null) return;

        float targetDistance = normalDistance;

        Vector3 worldDir = playerCamera.transform.TransformDirection(_localDirection);
        if (Physics.Raycast(playerCamera.transform.position, worldDir, out RaycastHit hit, normalDistance, wallMask, QueryTriggerInteraction.Ignore))
        {
            targetDistance = Mathf.Clamp(hit.distance - 0.05f, minDistance, normalDistance);
        }

        _currentDistance = Mathf.Lerp(_currentDistance, targetDistance, smoothing * Time.deltaTime);
        transform.localPosition = _localDirection * _currentDistance;
    }
}
