using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] // Asegura que haya un AudioSource en el objeto
public class Interactable : MonoBehaviour
{
    public string interactionMessage = "Has interactuado con el objeto.";
    public List<GameObject> objectsToDisable; // Lista de objetos para desactivar

    private AudioSource audioSource;

    private void Start()
    {
        // Obtener el AudioSource del objeto
        audioSource = GetComponent<AudioSource>();

        // Asegurar que el audio se reproduzca en bucle
        if (audioSource != null)
        {
            audioSource.loop = true; // Activar loop
            audioSource.playOnAwake = false; // No empezar automáticamente
        }
    }

    public virtual void Interact()
    {
        // Muestra el mensaje de interacción en la consola
        Debug.Log(interactionMessage);
        FindObjectOfType<UI_Mision>().ActualizarMision("Recupera los 5 artefactos");

        // Reproducir sonido en bucle
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        // Desactiva los objetos de la lista
        foreach (GameObject obj in objectsToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(false);
                Debug.Log($"{obj.name} ha sido desactivado.");

                // Guardar el estado del objeto desactivado
                GlobalGameState.SaveObjectState(obj);
            }
        }
    }
}
