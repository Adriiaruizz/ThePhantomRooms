using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // Para cargar la escena de fin de juego

public class Dolly : MonoBehaviour
{
    public Transform player;   // Referencia al jugador
    public float stopDistance = 1f; // Distancia m�nima para detenerse
    private Renderer enemyRenderer;
    private bool isChasing = false;

    private Animator animator; // Referencia al componente Animator
    private NavMeshAgent navMeshAgent; // Referencia al NavMeshAgent

    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>(); // Obtener el Animator
        navMeshAgent = GetComponent<NavMeshAgent>(); // Obtener el NavMeshAgent
    }

    void Update()
    {
        if (enemyRenderer.isVisible)
        {
            // Detener el movimiento cuando el enemigo est� visible en la c�mara
            isChasing = false;
            navMeshAgent.isStopped = true; // Detener al NavMeshAgent cuando est� visible
        }
        else
        {
            // Empezar a perseguir al jugador si no est� visible
            isChasing = true;
            navMeshAgent.isStopped = false; // Reanudar el movimiento del NavMeshAgent
        }

        // Actualizar el par�metro "isChasing" en el Animator
        animator.SetBool("isChasing", isChasing);

        if (isChasing)
        {
            // Mover al enemigo hacia el jugador usando el NavMeshAgent
            navMeshAgent.SetDestination(player.position);
        }
    }

    // Detectar la colisi�n con el jugador
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Terminar el juego cuando el jugador entra en el collider del enemigo
            Debug.Log("�El jugador ha muerto!");
            Time.timeScale = 0;
            // Aqu� puedes cargar una escena de fin de juego
            //SceneManager.LoadScene("GameOver"); // Reemplaza "GameOver" con el nombre de tu escena de fin de juego
        }
    }
}
