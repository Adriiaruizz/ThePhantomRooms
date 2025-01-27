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
    private Animator animator; // Referencia al Animator
    private Collider enemyCollider; // Referencia al Collider del enemigo

    private void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>(); // Obtén el Animator
        enemyCollider = GetComponent<Collider>(); // Obtén el Collider del enemigo

        if (patrolPoints.Length > 0)
        {
            navMeshAgent.destination = patrolPoints[currentPatrolIndex].position; // Establece el primer destino
            navMeshAgent.speed = patrolSpeed; // Establece la velocidad de patrulla
            animator.SetBool("isChasing", false); // Inicia en modo patrulla
        }
    }

    private void Update()
    {
        if (animator.GetBool("Muerte")) // Verifica si el enemigo está en estado de muerte
        {
            EnterDeathState();
            return; // No ejecuta más lógica si está en estado de muerte
        }

        if (flashlight.enabled)
        {
            StartChasingPlayer();
        }
        else
        {
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
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
        }
    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            navMeshAgent.destination = player.position;
        }
    }

    private void StartChasingPlayer()
    {
        if (!isChasing)
        {
            isChasing = true;
            navMeshAgent.speed = chaseSpeed;
            animator.SetBool("isChasing", true); // Cambia a la animación de persecución
            Debug.Log("El enemigo ha comenzado a perseguir al jugador.");
        }
    }

    private void StopChasingPlayer()
    {
        if (isChasing)
        {
            isChasing = false;
            navMeshAgent.speed = patrolSpeed;
            if (patrolPoints.Length > 0)
            {
                navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
            }
            animator.SetBool("isChasing", false); // Cambia a la animación de patrulla
            Debug.Log("El enemigo ha dejado de perseguir al jugador.");
        }
    }

    private void EnterDeathState()
    {
        navMeshAgent.isStopped = true; // Detiene el movimiento del NavMeshAgent
        navMeshAgent.velocity = Vector3.zero; // Asegura que el enemigo no se deslice

        if (enemyCollider != null)
        {
            enemyCollider.enabled = false; // Desactiva las colisiones del enemigo
        }

        Debug.Log("El enemigo ha entrado en estado de muerte.");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡El enemigo ha matado al jugador!");
            Time.timeScale = 0;
        }
    }
}
