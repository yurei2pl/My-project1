using UnityEngine;
using TMPro;

public class DoorInteract : MonoBehaviour
{
    [SerializeField] private GameObject promptText;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private string openBoolName = "isOpen";

    private bool playerInRange;

    private void Start()
    {
        if (promptText != null)
            promptText.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            doorAnimator.SetBool(openBoolName, true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (promptText != null)
                promptText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (promptText != null)
                promptText.SetActive(false);
        }
    }
}