using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Necesario para manejar UI

public class Dolly : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float stopDistance = 1f; // Distancia mínima para detenerse
    private Renderer enemyRenderer;
    private bool isChasing = false;
    private bool hasCaughtPlayer = false; // Evita que el jumpscare se active varias veces

    private Animator animator; // Referencia al Animator
    private NavMeshAgent navMeshAgent; // Referencia al NavMeshAgent
    private AudioSource audioSource; // Fuente de audio
    private Collider enemyCollider; // Referencia al Collider del enemigo

    public AudioClip chaseSound; // Sonido de persecución
    public GameObject jumpscareCanvas; // Canvas del jumpscare
    public GameObject gameOverCanvas; // Canvas del menú de Game Over
    public AudioClip jumpscareSound; // Sonido del jumpscare

    public Button restartButton; // Botón de reintentar
    public Button exitButton; // Botón de salir

    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        enemyCollider = GetComponent<Collider>(); // Obtener el Collider del enemigo

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = chaseSound;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        // Desactivar los Canvas al inicio
        if (jumpscareCanvas != null) jumpscareCanvas.SetActive(false);
        if (gameOverCanvas != null) gameOverCanvas.SetActive(false);

        // Esconder y bloquear el cursor al inicio
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Asignar eventos a los botones si existen
        if (restartButton != null) restartButton.onClick.AddListener(RestartGame);
        if (exitButton != null) exitButton.onClick.AddListener(ExitGame);
    }

    void Update()
    {
        if (hasCaughtPlayer) return; // Si el enemigo atrapó al jugador, no seguir moviéndose

        if (enemyRenderer.isVisible)
        {
            isChasing = false;
            navMeshAgent.isStopped = true;
        }
        else
        {
            isChasing = true;
            navMeshAgent.isStopped = false;
        }

        animator.SetBool("isChasing", isChasing);

        if (isChasing)
        {
            navMeshAgent.SetDestination(player.position);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasCaughtPlayer) return; // Evita que se active varias veces

        if (other.CompareTag("Player"))
        {
            hasCaughtPlayer = true; // Marcar que el jugador ha sido atrapado
            navMeshAgent.isStopped = true; // Detener al enemigo
            enemyCollider.enabled = false; // Desactivar el collider del enemigo

            if (jumpscareCanvas != null) jumpscareCanvas.SetActive(true);

            if (jumpscareSound != null)
            {
                audioSource.clip = jumpscareSound;
                audioSource.loop = false;
                audioSource.Play();

                Invoke("ShowGameOver", jumpscareSound.length);
            }
            else
            {
                ShowGameOver();
            }
        }
    }

    void ShowGameOver()
    {
        if (jumpscareCanvas != null) jumpscareCanvas.SetActive(false);
        if (gameOverCanvas != null) gameOverCanvas.SetActive(true);

        // Habilitar el cursor para interactuar con los botones
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Ocultar y bloquear el cursor nuevamente al reiniciar
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }
}
