using UnityEngine;

public class NotePickup : MonoBehaviour
{
    [SerializeField] private GameObject promptObject;
    [SerializeField] private GameObject noteModelPrefab; // model do pokazania graczowi
    [SerializeField] private string noteText; // opcjonalnie, jeśli masz UI z tekstem

    private bool inRange = false;
    private NoteViewer noteViewer;

    void Start()
    {
        if (promptObject != null) promptObject.SetActive(false);
    }

    void Update()
    {
        if (inRange && noteViewer != null && Input.GetKeyDown(KeyCode.E))
        {
            noteViewer.ShowNote(noteModelPrefab, noteText);
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            noteViewer = other.GetComponentInParent<NoteViewer>();
            inRange = true;
            if (promptObject != null) promptObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            if (promptObject != null) promptObject.SetActive(false);
        }
    }
}