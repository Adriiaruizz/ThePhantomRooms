using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombilla : MonoBehaviour
{
    public List<Light> lights; // Lista de luces seleccionables
    public Animator enemyAnimator; // Referencia al Animator del enemigo
    public GameObject prefab; // Prefab seleccionable que cambiará de color
    public GameObject portal; // Referencia al portal
    public string deathAnimationBool = "muerte"; // Nombre del condicional booleano en el Animator

    private bool isInteractable = true; // Indica si el objeto es interactuable

    void Update()
    {
        // Detectar interacción (puedes ajustar esto según cómo manejes la interacción)
        if (isInteractable && Input.GetKeyDown(KeyCode.E)) // Por defecto, usa la tecla "E"
        {
            Interact();
        }
    }

    void Interact()
    {
        // Activar las luces de la lista
        if (lights != null && lights.Count > 0)
        {
            foreach (Light light in lights)
            {
                if (light != null)
                {
                    light.enabled = true;
                }
            }
        }

        // Activar el booleano "muerte" en el Animator del enemigo
        if (enemyAnimator != null && enemyAnimator.HasParameter(deathAnimationBool))
        {
            enemyAnimator.SetBool(deathAnimationBool, true);
        }

        // Cambiar el color del prefab a azul
        if (prefab != null)
        {
            Renderer prefabRenderer = prefab.GetComponent<Renderer>();
            if (prefabRenderer != null)
            {
                prefabRenderer.material.color = Color.blue;
            }
        }

        // Activar el portal
        if (portal != null)
        {
            portal.SetActive(true);
        }

        // Apagar este objeto
        isInteractable = false;
        gameObject.SetActive(false);
    }
}

// Extensión para verificar si un parámetro existe en el Animator
public static class AnimatorExtensions
{
    public static bool HasParameter(this Animator animator, string parameterName)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == parameterName)
                return true;
        }
        return false;
    }
}
