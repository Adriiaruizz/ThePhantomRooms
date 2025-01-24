using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Tooltip("Nombre de la escena a la que teletransportará el portal.")]
    public string targetScene; // Nombre de la escena de destino

    [Tooltip("Tiempo en segundos antes de cargar la nueva escena.")]
    public float delayBeforeTeleport = 0f; // Retraso opcional antes de cargar la escena

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entra al portal es el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador ha entrado al portal.");

            // Inicia el teletransporte con retraso si está configurado
            if (delayBeforeTeleport > 0)
            {
                StartCoroutine(TeleportWithDelay());
            }
            else
            {
                Teleport();
            }
        }
    }

    private void Teleport()
    {
        if (!string.IsNullOrEmpty(targetScene))
        {
            Debug.Log("Cambiando a la escena: " + targetScene);
            SceneManager.LoadScene(targetScene);
        }
        else
        {
            Debug.LogError("No se ha especificado una escena de destino en el portal.");
        }
    }

    private IEnumerator TeleportWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeTeleport);
        Teleport();
    }
}
