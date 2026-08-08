using UnityEngine;

// Wszystko z czym gracz może wejść w interakcję (E) implementuje to.
public interface IInteractable
{
    // Wywoływane gdy gracz patrzy na obiekt i wciśnie E
    void Interact(GameObject interactor);

    // Tekst do promptu UI, np. "Otwórz drzwi", "Podnieś"
    string InteractionPrompt { get; }

    // Pozwala obiektowi tymczasowo zablokować interakcję (np. zamek, cooldown)
    bool CanInteract { get; }
}
