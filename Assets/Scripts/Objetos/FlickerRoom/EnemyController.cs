using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float speed = 3f; // Velocidad de movimiento
    public float killDistance = 1.5f; // Distancia mínima para "matar" al jugador

    private bool isChasing = false; // Estado de persecución

    void Update()
    {
        if (isChasing && player != null)
        {
            // Mover hacia el jugador
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Comprobar si el enemigo alcanza al jugador
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= killDistance)
            {
                KillPlayer();
            }
        }
    }

    public void StartChasing(Transform target)
    {
        player = target;
        isChasing = true;
    }

    private void KillPlayer()
    {
        Debug.Log("El jugador ha sido alcanzado y eliminado.");
        // Detener el tiempo del juego
        Time.timeScale = 0;

        // Opcional: Mostrar un mensaje de Game Over
        // Puedes implementar una UI para mostrar un mensaje al jugador
    }
}
