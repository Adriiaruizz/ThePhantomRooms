using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioPrefab : MonoBehaviour
{
    public GameObject prefabDesactivar; // Prefab que se desactiva
    public GameObject prefabActivar;   // Prefab que se activa

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tiene la etiqueta "Player"
        {
            if (prefabDesactivar != null)
                prefabDesactivar.SetActive(false);

            if (prefabActivar != null)
                prefabActivar.SetActive(true);
        }
    }
}
