using UnityEngine;

public class GeneradoresFinales : MonoBehaviour
{
    private bool isActivated = false;
    private bool playerInside = false;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        if (!isActivated)
        {
            isActivated = true;
            ControlladorGeneradores.Instance.ObjectActivated(); // Notifica al GameManager
            gameObject.SetActive(false); // Desactiva el objeto interactuado
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}
