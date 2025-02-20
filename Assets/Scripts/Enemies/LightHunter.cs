using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LightHunter : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public Transform player;
    public Light flashlight;
    public AudioClip sonidoPatrullaje;
    public float maxVolumeDistance = 10f;
    public float minVolumeDistance = 50f;
    public Canvas jumpscareCanvas;
    public AudioClip jumpscareSound;
    public Canvas gameOverCanvas;

    public Button restartButton; // Referencia al botón de reinicio
    public Button exitButton; // Referencia al botón de salida

    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private bool hasTriggeredJumpscare = false;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Animator animator;
    private Collider enemyCollider;
    private AudioSource audioSource;

    private void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider>();
        audioSource = gameObject.AddComponent<AudioSource>();

        jumpscareCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);

        // Asignar los eventos de los botones para reiniciar o salir
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }

        // Bloquear el cursor al iniciar la partida
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (patrolPoints.Length > 0)
        {
            navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
            navMeshAgent.speed = patrolSpeed;
            animator.SetBool("isChasing", false);
        }

        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (hasTriggeredJumpscare) return; // Si el jumpscare ya se activó, no hacer nada más

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
            PlaySound(true);
        }
        else
        {
            ChasePlayer();
            PlaySound(true);
        }

        AdjustSoundVolume();
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
            animator.SetBool("isChasing", true);
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
            animator.SetBool("isChasing", false);
            Debug.Log("El enemigo ha dejado de perseguir al jugador.");
        }
    }

    private void PlaySound(bool play)
    {
        if (play && !audioSource.isPlaying && sonidoPatrullaje != null)
        {
            audioSource.clip = sonidoPatrullaje;
            audioSource.Play();
        }
        else if (!play && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void AdjustSoundVolume()
    {
        if (audioSource.isPlaying)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            float volume = Mathf.Clamp01(1 - (distance - maxVolumeDistance) / (minVolumeDistance - maxVolumeDistance));
            audioSource.volume = volume;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasTriggeredJumpscare)
        {
            hasTriggeredJumpscare = true;
            Debug.Log("¡El enemigo ha matado al jugador!");
            TriggerJumpscare();
        }
    }

    private void TriggerJumpscare()
    {
        jumpscareCanvas.gameObject.SetActive(true);

        if (jumpscareSound != null)
        {
            AudioSource.PlayClipAtPoint(jumpscareSound, transform.position);
        }

        // Detener al enemigo
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;

        // Desactivar controles del jugador si tiene un script de movimiento
        if (player.GetComponent<Movimiento>() != null)
        {
            player.GetComponent<Movimiento>().enabled = false;
        }

        // Detener el tiempo del juego
        Time.timeScale = 0;

        StartCoroutine(DisableJumpscare());
    }

    private IEnumerator DisableJumpscare()
    {
        yield return new WaitForSecondsRealtime(2f);
        jumpscareCanvas.gameObject.SetActive(false);
        ShowGameOverScreen();
    }

    private void ShowGameOverScreen()
    {
        gameOverCanvas.gameObject.SetActive(true);

        // Desbloquear el cursor para que el jugador pueda usar los botones
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Método para reiniciar la escena
    public void RestartGame()
    {
        Time.timeScale = 1; // Reactivar el tiempo antes de reiniciar

        // Bloquear el cursor al reiniciar la partida
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Método para salir del juego
    public void ExitGame()
    {
        Application.Quit();
    }
}
