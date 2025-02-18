using UnityEngine;

public class Cheese : MonoBehaviour
{
    public GameObject prefabAActivar; // El prefab que se activar�
    private bool jugadorCerca = false; // Para controlar si el jugador est� cerca

    void Update()
    {
        // Si el jugador est� cerca y se presiona la tecla E, activamos el prefab y desactivamos el objeto actual
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            ActivarYDesactivarPrefab();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificamos si el jugador entra en el �rea del trigger
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true; // El jugador est� cerca del trigger
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verificamos si el jugador sale del �rea del trigger
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false; // El jugador ya no est� cerca
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
