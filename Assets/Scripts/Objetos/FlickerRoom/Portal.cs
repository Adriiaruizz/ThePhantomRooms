using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor; // Necesario para SceneAsset
#endif

public class ColorBasedCollider : MonoBehaviour
{
    public Renderer objectRenderer; // Renderer del objeto
    public Collider physicalCollider; // Collider físico
    public Collider triggerCollider; // Collider de tipo trigger

#if UNITY_EDITOR
    public SceneAsset sceneAsset; // Permite arrastrar la escena desde el editor
#endif
    private string sceneToLoad; // Nombre de la escena a cargar

    private void Start()
    {
#if UNITY_EDITOR
        if (sceneAsset != null)
        {
            // Obtiene el nombre de la escena seleccionada
            sceneToLoad = sceneAsset.name;
        }
#else
        Debug.LogWarning("El script solo obtiene el nombre de la escena desde el editor.");
#endif

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

            // Marca el booleano en la clase estática
            GlobalGameState.FlickerRoomCompleta = true;

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
