using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombilla : MonoBehaviour
{
    public Light sceneLight; // La luz que se activará
    public GameObject enemy; // El enemigo que entrará en estado "Muerte"
    public string enemyDeathState = "Death"; // Nombre del estado de "Muerte" en el Animator del enemigo

    private Animator enemyAnimator; // Referencia al Animator del enemigo

    private void Start()
    {
        // Asegúrate de que la luz esté inicialmente desactivada
        if (sceneLight != null)
            sceneLight.enabled = false;

        // Busca el Animator en el enemigo, si existe
        if (enemy != null)
            enemyAnimator = enemy.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el jugador interactúa con el objeto
        if (other.CompareTag("Player"))
        {
            ActivateInteraction();
        }
    }

    private void ActivateInteraction()
    {
        // Activa la luz
        if (sceneLight != null)
        {
            sceneLight.enabled = true;
        }

        // Cambia al estado "Muerte" del enemigo
        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger(enemyDeathState);
        }

        // Desactiva el movimiento del enemigo
        EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            enemyAI.enabled = false;
        }

        // Desactiva este objeto interactuable
        gameObject.SetActive(false);

        Debug.Log("Interacción completada. Luz activada, enemigo desactivado, y objeto eliminado.");
    }
}
