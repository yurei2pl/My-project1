using UnityEngine;

// Podpięte pod InteractionController (raycast + E).
// noteModelInScene to gotowy obiekt modelu kartki, wcześniej ustawiony ręcznie w scenie
// (np. dziecko kamery, pozycja przed graczem), domyślnie wyłączony (SetActive(false) w edytorze).
public class NotePickup : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject noteModelInScene;
    [SerializeField] private string noteText;
    [SerializeField] private string prompt = "Przeczytaj notatkę";

    public string InteractionPrompt => prompt;
    public bool CanInteract => true;

    public void Interact(GameObject interactor)
    {
        NoteViewer noteViewer = interactor.GetComponentInChildren<NoteViewer>();
        if (noteViewer == null) return;

        noteViewer.ShowNote(noteModelInScene, noteText);
        gameObject.SetActive(false);
    }
}
