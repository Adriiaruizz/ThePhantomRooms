using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float speed = 3f; // Velocidad de movimiento
    public float rotationSpeed = 5f; // Velocidad de rotaci�n hacia el jugador
    public float killDistance = 1.5f; // Distancia m�nima para "matar" al jugador
    public Animator animator; // Referencia al Animator del enemigo

    private bool isChasing = false; // Estado de persecuci�n

    void Start()
    {
        // Si no hay un Animator asignado, intenta obtenerlo autom�ticamente
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (isChasing && player != null)
        {
            // Cambia a la animaci�n de correr
            animator.SetBool("isRunning", true);

            // Gira hacia el jugador
            RotateTowardsPlayer();

            // Mover hacia el jugador
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Comprobar si el enemigo alcanza al jugador
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= killDistance)
            {
                KillPlayer();
            }
        }
        else
        {
            // Si no est� persiguiendo al jugador, mant�n la animaci�n en idle
            animator.SetBool("isRunning", false);
        }
    }

    public void StartChasing(Transform target)
    {
        player = target;
        isChasing = true;
    }

    private void RotateTowardsPlayer()
    {
        // Calcula la direcci�n hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;

        // Calcula la rotaci�n necesaria para mirar hacia el jugador
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Suaviza la rotaci�n hacia el jugador
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
