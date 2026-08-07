using UnityEngine;

public class NoteViewer : MonoBehaviour
{
    [SerializeField] private Transform viewPoint; // pusty obiekt przed kamerą
    [SerializeField] private KeyCode closeKey = KeyCode.E;
    [SerializeField] private MonoBehaviour[] disableWhileReading; // np. PlayerMovement, CameraLook

    private GameObject currentNoteInstance;
    private bool isReading = false;

    void Update()
    {
        if (isReading && Input.GetKeyDown(closeKey))
        {
            CloseNote();
        }
    }

    public void ShowNote(GameObject notePrefab, string text)
    {
        if (isReading) return;

        currentNoteInstance = Instantiate(notePrefab, viewPoint.position, viewPoint.rotation, viewPoint);
        isReading = true;

        SetInputEnabled(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // jeśli masz UI tekstowe:
        // NoteTextUI.Instance.Show(text);
    }

    void CloseNote()
    {
        if (currentNoteInstance != null) Destroy(currentNoteInstance);
        isReading = false;

        SetInputEnabled(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // NoteTextUI.Instance.Hide();
    }

    void SetInputEnabled(bool state)
    {
        foreach (var c in disableWhileReading)
            c.enabled = state;
    }
}