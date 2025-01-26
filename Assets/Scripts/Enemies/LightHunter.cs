using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHunter : MonoBehaviour
{
    public Transform[] patrolPoints; // Array de puntos de patrulla
    public float patrolSpeed = 2f; // Velocidad de patrulla
    public float chaseSpeed = 4f; // Velocidad de persecución
    public Transform player; // Referencia al jugador
    public Light flashlight; // Linterna del jugador

    private int currentPatrolIndex = 0; // Índice del punto de patrulla actual
    private bool isChasing = false; // Si el enemigo está persiguiendo al jugador
    private UnityEngine.AI.NavMeshAgent navMeshAgent; // Agente de navegación

    private void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (patrolPoints.Length > 0)
        {
            navMeshAgent.destination = patrolPoints[currentPatrolIndex].position; // Establece el primer destino
            navMeshAgent.speed = patrolSpeed; // Establece la velocidad de patrulla
        }
    }

    private void Update()
    {
        if (flashlight.enabled)
        {
            // Si la linterna está encendida, activa la persecución
            StartChasingPlayer();
        }
        else
        {
            // Si la linterna está apagada, vuelve a patrullar
            StopChasingPlayer();
        }

        if (!isChasing)
        {
            Patrol();
        }
        else
        {
            ChasePlayer();
        }
    }

    private void Patrol()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            // Cambia al siguiente punto de patrulla
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
        }
    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            navMeshAgent.destination = player.position; // Sigue al jugador
        }
    }

    private void StartChasingPlayer()
    {
        if (!isChasing)
        {
            isChasing = true;
            navMeshAgent.speed = chaseSpeed; // Cambia la velocidad a la de persecución
            Debug.Log("El enemigo ha comenzado a perseguir al jugador.");
        }
    }

    private void StopChasingPlayer()
    {
        if (isChasing)
        {
            isChasing = false;
            navMeshAgent.speed = patrolSpeed; // Vuelve a la velocidad de patrulla
            if (patrolPoints.Length > 0)
            {
                navMeshAgent.destination = patrolPoints[currentPatrolIndex].position; // Retoma la patrulla
            }
            Debug.Log("El enemigo ha dejado de perseguir al jugador.");
        }
    }

    // Detectar la colisión con el jugador
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Si el enemigo toca al jugador, detén el juego
            Debug.Log("¡El enemigo ha matado al jugador!");
            Time.timeScale = 0; // Detener el juego
        }
    }
}
