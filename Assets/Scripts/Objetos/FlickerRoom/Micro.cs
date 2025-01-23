using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable2 : MonoBehaviour
{
    public string interactionMessage = "Has interactuado con el objeto.";
    public GameObject enemy; // Referencia al enemigo
    public Transform player; // Referencia al jugador
    public GameObject objectToActivate; // Objeto que se activará para desactivar los prefabs

    public virtual void Interact()
    {
        // Muestra el mensaje de interacción en la consola
        Debug.Log(interactionMessage);

        // Si el enemigo está asignado, activa su script de persecución
        if (enemy != null && player != null)
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.StartChasing(player); // Activa la persecución
                Debug.Log("El enemigo ha comenzado a perseguir al jugador.");
            }
        }

        // Activa el objeto para realizar una acción adicional
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
