using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactionMessage = "Has interactuado con el objeto.";
    public List<GameObject> objectsToDisable; // Lista de objetos para desactivar

    public virtual void Interact()
    {
        // Muestra el mensaje de interacción en la consola
        Debug.Log(interactionMessage);

        // Desactiva los objetos de la lista
        foreach (GameObject obj in objectsToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(false);
                Debug.Log($"{obj.name} ha sido desactivado.");
            }
        }
    }
}
