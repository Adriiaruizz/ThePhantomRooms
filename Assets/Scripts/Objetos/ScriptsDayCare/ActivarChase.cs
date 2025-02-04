using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarChase : MonoBehaviour
{
    public GameObject prefabActivar; // Prefab que se activará

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Si el jugador entra en el trigger
        {
            if (prefabActivar != null)
            {
                prefabActivar.SetActive(true); // Activa el prefab
                Debug.Log(prefabActivar.name + " ha sido activado.");
            }
            else
            {
                Debug.LogWarning("No se ha asignado un prefab para activar en " + gameObject.name);
            }
        }
    }
}
