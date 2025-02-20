using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI; // Necesario para UI

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Idle, Patrol, Chase, Attack }
    public EnemyState currentState = EnemyState.Idle;

    public Transform player;
    public Transform[] patrolPoints;
    public float detectionRadius = 10f;
    public float attackRadius = 2f;
    public float stopChaseDistance = 12f;
    public float idleTime = 2f;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private int currentPatrolIndex = 0;
    private float idleTimer = 0f;

    public AudioSource audioSource;
    public AudioClip sonidoEnemigo;

    public Image screamerImage;
    public AudioClip screamerSound;

    // Referencias UI
    public GameObject gameOverScreen; // Pantalla Game Over
    public Button restartButton; // Botón Reiniciar
    public Button exitButton; // Botón Abandonar
    public Canvas uiCanvas; // Referencia al Canvas que quieres desactivar

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        StartCoroutine(EnemySoundRoutine());
        TransitionToState(EnemyState.Idle);

        if (screamerImage != null)
        {
            screamerImage.enabled = false; // Ocultar screamer al inicio
        }

        // Inicializar la pantalla de Game Over
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false); // Ocultar pantalla de Game Over al inicio
        }

        // Asignar los eventos a los botones
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }
    }

    private void Update()
    {
        // Comprobación de la distancia del jugador para determinar el cambio de estado
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius && currentState != EnemyState.Chase && currentState != EnemyState.Attack)
        {
            TransitionToState(EnemyState.Chase);
        }

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
            StartCoroutine(ShowScreamer()); // Llama al screamer en lugar de detener el juego
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
                navMeshAgent.isStopped = true;
                break;
        }
    }

    private IEnumerator ShowScreamer()
    {
        if (screamerImage != null)
        {
            screamerImage.enabled = true; // Mostrar screamer
        }

        if (audioSource != null && screamerSound != null)
        {
            audioSource.PlayOneShot(screamerSound); // Reproducir sonido de screamer
        }

        yield return new WaitForSeconds(1f); // Esperar 1 segundo

        // Desactivar el Canvas
        if (uiCanvas != null)
        {
            uiCanvas.enabled = false; // Desactivar el Canvas
        }

        // Detener el juego, pero permitir el uso del ratón
        Time.timeScale = 0f; // Detener el tiempo en el juego
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true); // Mostrar pantalla de Game Over
        }

        // Habilitar la interacción con la UI aunque el tiempo esté detenido
        Cursor.lockState = CursorLockMode.None;  // Desbloquear el ratón
        Cursor.visible = true;  // Asegurarse de que el ratón sea visible
    }

    private void RestartGame()
    {
        // Reiniciar la escena actual
        Time.timeScale = 1f; // Asegurarse de que el tiempo se reanude
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ExitGame()
    {
        // Salir del juego
        Application.Quit();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    private IEnumerator EnemySoundRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);

            if (audioSource != null && sonidoEnemigo != null)
            {
                audioSource.PlayOneShot(sonidoEnemigo);
            }
        }
    }
}
