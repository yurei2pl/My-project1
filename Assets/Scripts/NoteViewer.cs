using UnityEngine;
using TMPro;

// Model notatki NIE jest już tworzony (Instantiate) - to gotowy obiekt w scenie
// (np. dziecko kamery, ustawiony ręcznie przed kamerą), który po prostu włączamy/wyłączamy.
public class NoteViewer : MonoBehaviour
{
    [SerializeField] private KeyCode closeKey = KeyCode.E;
    [SerializeField] private MonoBehaviour[] disableWhileReading; // np. FirstPersonController, StarterAssetsInputs

    [Header("UI tekstu")]
    [SerializeField] private GameObject textPanel;
    [SerializeField] private TMP_Text noteTextUI;

    private GameObject currentNoteModel;
    private bool isReading = false;
    private int openedFrame = -1;

    private void Start()
    {
        if (textPanel != null) textPanel.SetActive(false);
    }

    void Update()
    {
        // openedFrame chroni przed zamknięciem notatki w tej samej klatce co jej otwarcie
        // (ten sam klawisz E jest użyty do podniesienia i do zamknięcia)
        if (isReading && Time.frameCount > openedFrame && Input.GetKeyDown(closeKey))
        {
            CloseNote();
        }
    }

    // noteModel = konkretny obiekt już istniejący w scenie (np. dziecko kamery), nie prefab.
    public void ShowNote(GameObject noteModel, string text)
    {
        if (isReading) return;

        currentNoteModel = noteModel;
        if (currentNoteModel != null) currentNoteModel.SetActive(true);

        isReading = true;
        openedFrame = Time.frameCount;

        SetInputEnabled(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (textPanel != null) textPanel.SetActive(true);
        if (noteTextUI != null) noteTextUI.text = text;
    }

    void CloseNote()
    {
        if (currentNoteModel != null) currentNoteModel.SetActive(false);
        currentNoteModel = null;

        isReading = false;

        SetInputEnabled(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (textPanel != null) textPanel.SetActive(false);
    }

    void SetInputEnabled(bool state)
    {
        foreach (var c in disableWhileReading)
            if (c != null) c.enabled = state;
    }
}
