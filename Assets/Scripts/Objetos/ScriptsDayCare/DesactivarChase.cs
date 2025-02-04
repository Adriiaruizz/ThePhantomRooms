using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarChase : MonoBehaviour
{
    public GameObject prefabDesactivar; // Prefab que se activará

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Si el jugador entra en el trigger
        {
            if (prefabDesactivar != null)
            {
                prefabDesactivar.SetActive(false); // Activa el prefab
                Debug.Log(prefabDesactivar.name + " ha sido activado.");
            }
            else
            {
                Debug.LogWarning("No se ha asignado un prefab para desactivar en " + gameObject.name);
            }
        }
    }
}
