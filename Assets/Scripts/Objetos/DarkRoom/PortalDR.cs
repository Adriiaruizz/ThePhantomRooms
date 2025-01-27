using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class PortalDR : MonoBehaviour
{
    private Renderer portalRenderer; // Renderer del portal
    private Collider portalCollider; // Collider físico del portal
    private Color targetColor = Color.blue; // Color objetivo para desactivar el collider

    public string targetScene; // Nombre de la escena a cargar
    

    void Start()
    {
        // Obtener el renderer y el collider del portal
        portalRenderer = GetComponent<Renderer>();
        portalCollider = GetComponent<Collider>();

        if (portalRenderer == null)
        {
            Debug.LogError("No se encontró el Renderer en el portal.");
        }

        if (portalCollider == null)
        {
            Debug.LogError("No se encontró el Collider en el portal.");
        }

        if (portalCollider != null && !portalCollider.isTrigger)
        {
            Debug.LogError("El collider del portal debe configurarse como Trigger.");
        }
    }

    void Update()
    {
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra es el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("El jugador ha entrado en el portal.");
            GlobalGameState.DarkRoomsCompletas = true;
            SceneManager.LoadScene(targetScene); // Cambiar a la escena objetivo
        }
    }
}
