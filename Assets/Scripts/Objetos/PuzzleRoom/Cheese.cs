using UnityEngine;

public class Cheese : MonoBehaviour
{
    public GameObject prefabAActivar; // El prefab que se activará
    private bool jugadorCerca = false; // Para controlar si el jugador está cerca

    void Update()
    {
        // Si el jugador está cerca y se presiona la tecla E, activamos el prefab y desactivamos el objeto actual
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            ActivarYDesactivarPrefab();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificamos si el jugador entra en el área del trigger
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true; // El jugador está cerca del trigger
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verificamos si el jugador sale del área del trigger
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false; // El jugador ya no está cerca
        }
    }

    void ActivarYDesactivarPrefab()
    {
        if (prefabAActivar != null)
        {
            prefabAActivar.SetActive(true); // Activamos el prefab
        }

        gameObject.SetActive(false); // Desactivamos el objeto que lleva el script
    }
}
