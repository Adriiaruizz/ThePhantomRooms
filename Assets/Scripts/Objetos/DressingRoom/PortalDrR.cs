using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar escenas

public class PortalDrR : MonoBehaviour
{
    public string nextSceneName; // Nombre de la escena a cargar
    public string interactableTag = "Player"; // Solo el jugador puede activar el portal

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra tiene el tag correcto
        if (other.CompareTag(interactableTag))
        {
            Debug.Log("El jugador ha entrado al portal.");

            // Activar el booleano en GlobalGameState
            GlobalGameState.DressingRoomCompleta = true;
            Debug.Log("GlobalGameState: DressingRoomCompleta activado.");

            // Cambiar de escena
            ChangeScene();
        }
    }

    void ChangeScene()
    {
        // Verificar si el nombre de la escena está configurado
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("El nombre de la siguiente escena no está configurado.");
        }
    }
}
