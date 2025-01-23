using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable2 : MonoBehaviour
{
    public string interactionMessage = "Has interactuado con el objeto.";
    public GameObject enemy; // Referencia al enemigo
    public Transform player; // Referencia al jugador
    public GameObject objectToActivate; // Objeto que se activar� para desactivar los prefabs

    public virtual void Interact()
    {
        // Muestra el mensaje de interacci�n en la consola
        Debug.Log(interactionMessage);

        // Si el enemigo est� asignado, activa su script de persecuci�n
        if (enemy != null && player != null)
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.StartChasing(player); // Activa la persecuci�n
                Debug.Log("El enemigo ha comenzado a perseguir al jugador.");
            }
        }

        // Activa el objeto para realizar una acci�n adicional
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            Debug.Log($"{objectToActivate.name} ha sido activado.");
        }

        // Desactiva el objeto que contiene este script
        gameObject.SetActive(false);
        Debug.Log($"{gameObject.name} ha sido desactivado.");
    }
}
