using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorBasedCollider : MonoBehaviour
{
    public Renderer objectRenderer; // Renderer del objeto
    public Collider physicalCollider; // Collider físico
    public Collider triggerCollider; // Collider de tipo trigger

    [SerializeField]
    private string sceneToLoad; // Nombre de la escena a cargar (ingresado manualmente en el inspector)

    private void Start()
    {
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogError("El nombre de la escena no está configurado. Por favor, ingrésalo en el inspector.");
        }

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
        // Comprueba si el jugador toca el trigger
        if (triggerCollider.enabled && other.CompareTag("Player"))
        {
            if (string.IsNullOrEmpty(sceneToLoad))
            {
                Debug.LogError("El nombre de la escena no está configurado. Por favor, verifica el inspector.");
                return;
            }

            Debug.Log("Cambiando a la escena: " + sceneToLoad);

            // Marca el booleano en la clase estática
            GlobalGameState.FlickerRoomCompleta = true;

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
