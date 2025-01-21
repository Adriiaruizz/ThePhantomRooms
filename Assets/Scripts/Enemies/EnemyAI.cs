using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Idle, Patrol, Chase, Attack }
    public EnemyState currentState = EnemyState.Idle;

    public Transform player; // Referencia al player
    public Transform[] patrolPoints; // Puntos de patrulla
    public float detectionRadius = 10f; // Radio de detección
    public float attackRadius = 2f; // Distancia a la que ataca
    public float stopChaseDistance = 12f; // Distancia para detener la persecución
    public float idleTime = 2f; // Tiempo de espera en estado Idle

    private NavMeshAgent navMeshAgent;
    private Animator animator; // Referencia al Animator
    private int currentPatrolIndex = 0;
    private float idleTimer = 0f;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Obtén el componente Animator
        TransitionToState(EnemyState.Idle);
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Detectar al jugador y cambiar al estado de persecución si está en rango
        if (distanceToPlayer <= detectionRadius && currentState != EnemyState.Chase && currentState != EnemyState.Attack)
        {
            TransitionToState(EnemyState.Chase);
        }

        // Cambiar al estado correspondiente
        switch (currentState)
        {
            case EnemyState.Idle:
                HandleIdleState();
                break;
            case EnemyState.Patrol:
                HandlePatrolState();
                break;
            case EnemyState.Chase:
                HandleChaseState();
                break;
            case EnemyState.Attack:
                HandleAttackState();
                break;
        }

        // Actualizar el parámetro Speed del Animator
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }

    private void HandleIdleState()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleTime)
        {
            TransitionToState(EnemyState.Patrol);
        }
    }

    private void HandlePatrolState()
    {
        if (patrolPoints.Length == 0) return;

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    private void HandleChaseState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRadius)
        {
            TransitionToState(EnemyState.Attack);
        }
        else if (distanceToPlayer > stopChaseDistance)
        {
            TransitionToState(EnemyState.Idle);
        }
        else
        {
            navMeshAgent.SetDestination(player.position);
        }
    }

    private void HandleAttackState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRadius)
        {
            TransitionToState(EnemyState.Chase);
        }
        else
        {
            // Si el enemigo está en rango, termina el juego
            Debug.Log("El jugador ha sido alcanzado. Fin del juego.");
            EndGame();
        }
    }

    private void TransitionToState(EnemyState newState)
    {
        currentState = newState;
        navMeshAgent.isStopped = false;

        switch (newState)
        {
            case EnemyState.Idle:
                navMeshAgent.ResetPath();
                idleTimer = 0f;
                break;
            case EnemyState.Patrol:
                if (patrolPoints.Length > 0)
                {
                    navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
                }
                break;
            case EnemyState.Chase:
                break;
            case EnemyState.Attack:
                navMeshAgent.isStopped = true; // Detén al enemigo durante el ataque
                break;
        }
    }

    private void EndGame()
    {
        // Detener toda la física y lógica del juego
        Time.timeScale = 0;

        // Opcional: Muestra un mensaje de "Game Over" en la consola
        Debug.Log("Game Over. El juego se ha detenido.");
    }

    private void OnDrawGizmosSelected()
    {
        // Dibujar radio de deteccion
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
