using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LamparasInteractable : MonoBehaviour
{
    public Light lampLight; // Referencia a la luz del objeto interactuable
    public GameObject prefabToDisable; // Prefab que ser� desactivado
    public List<GameObject> prefabsToActivate; // Lista de prefabs que se activar�n
    public string interactableTag = "Player"; // Solo objetos con este tag pueden interactuar
    public KeyCode interactionKey = KeyCode.E; // Tecla para interactuar
    private bool isInteractable = false; // Verifica si el objeto puede ser interactuado

    void Update()
    {
        // Detectar interacci�n solo si el jugador est� dentro del rango
        if (isInteractable && Input.GetKeyDown(interactionKey))
        {
            Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Activar interacci�n solo si el objeto tiene el tag especificado
        if (other.CompareTag(interactableTag))
        {
            isInteractable = true;
            Debug.Log("Jugador dentro del rango de interacci�n.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Desactivar interacci�n al salir del rango
        if (other.CompareTag(interactableTag))
        {
            isInteractable = false;
            Debug.Log("Jugador fuera del rango de interacci�n.");
        }
    }

    void Interact()
    {
        // Cambiar el color de la luz a verde
        if (lampLight != null)
        {
            lampLight.color = Color.green; // Cambia el color a verde
        }

        // Desactivar el prefab
        if (prefabToDisable != null)
        {
            prefabToDisable.SetActive(false); // Desactiva el prefab
        }

        // Activar los prefabs de la lista
        foreach (GameObject prefab in prefabsToActivate)
        {
            if (prefab != null)
            {
                prefab.SetActive(true);
                Debug.Log($"{prefab.name} ha sido activado.");
            }
        }

        // Hacer que el objeto no sea interactuable de nuevo
        isInteractable = false;

        Debug.Log("El objeto ha sido interactuado. El prefab se ha desactivado, los prefabs se han activado, y la luz es verde.");
    }
}
