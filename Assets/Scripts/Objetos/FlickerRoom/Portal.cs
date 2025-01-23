using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorBasedCollider : MonoBehaviour
{
    public Renderer objectRenderer; // Renderer del objeto
    public Collider physicalCollider; // Collider físico
    public Collider triggerCollider; // Collider de tipo trigger
    public string sceneToLoad = "NextScene"; // Nombre de la escena a cargar

    private void Start()
    {
        if (objectRenderer == null)
            objectRenderer = GetComponent<Renderer>();

        // Configuración inicial basada en el color actual
        UpdateCollidersBasedOnColor();
    }

    private void Update()
    {
        // Comprueba continuamente el color y actualiza los colliders si es necesario
        UpdateCollidersBasedOnColor();
    }

    private void UpdateCollidersBasedOnColor()
    {
        if (objectRenderer == null) return;

        // Obtiene el color actual del material
        Color currentColor = objectRenderer.material.color;

        if (currentColor == Color.red)
        {
            // Si es rojo, activa el collider físico y desactiva el trigger
            SetColliders(true);
        }
        else if (currentColor == Color.blue)
        {
            // Si es azul, activa el trigger y desactiva el collider físico
            SetColliders(false);
        }
    }

    private void SetColliders(bool isPhysical)
    {
        if (physicalCollider != null)
            physicalCollider.enabled = isPhysical;

        if (triggerCollider != null)
            triggerCollider.enabled = !isPhysical;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el jugador toca el trigger y carga la escena
        if (triggerCollider.enabled && other.CompareTag("Player"))
        {
            Debug.Log("Cambiando a la escena: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
