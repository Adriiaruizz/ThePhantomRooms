using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peluca : MonoBehaviour
{
    public GameObject prefabToActivate; // Prefab que será activado
    public string interactableTag = "Player"; // Solo el jugador puede interactuar
    public KeyCode interactionKey = KeyCode.E; // Tecla para interactuar
    private bool isInteractable = false; // Indica si se puede interactuar con la peluca

    void Update()
    {
        // Detectar interacción si el jugador está cerca
        if (isInteractable && Input.GetKeyDown(interactionKey))
        {
            Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Activar la capacidad de interactuar si el jugador entra en el rango
        if (other.CompareTag(interactableTag))
        {
            isInteractable = true;
            Debug.Log("Jugador dentro del rango de interacción de la peluca.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Desactivar la capacidad de interactuar al salir del rango
        if (other.CompareTag(interactableTag))
        {
            isInteractable = false;
            Debug.Log("Jugador fuera del rango de interacción de la peluca.");
        }
    }

    void Interact()
    {
        // Activar el prefab
        if (prefabToActivate != null)
        {
            prefabToActivate.SetActive(true);
            Debug.Log("Prefab activado.");
        }

        // Desactivar la peluca
        gameObject.SetActive(false);
        Debug.Log("Peluca desactivada.");
    }
}
