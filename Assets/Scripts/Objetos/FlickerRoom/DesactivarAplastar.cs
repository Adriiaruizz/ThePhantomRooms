using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAplastar : MonoBehaviour
{
    public string targetTag = "Aplastar"; // Tag de los objetos a desactivar

    private void OnEnable()
    {
        // Encuentra todos los objetos con el tag especificado
        GameObject[] objectsToDeactivate = GameObject.FindGameObjectsWithTag(targetTag);

        // Desactiva cada objeto encontrado
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(false);
            Debug.Log($"{obj.name} con tag '{targetTag}' ha sido desactivado.");
        }

        // Opcional: Desactiva este objeto tras realizar su acción
        gameObject.SetActive(false);
    }
}
