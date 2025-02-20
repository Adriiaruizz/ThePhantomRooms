using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cargar y manejar escenas
using UnityEngine.UI; // Necesario para trabajar con botones

public class EnemyController : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float speed = 3f; // Velocidad de movimiento
    public float rotationSpeed = 5f; // Velocidad de rotaci�n hacia el jugador
    public float killDistance = 1.5f; // Distancia m�nima para "matar" al jugador
    public Animator animator; // Referencia al Animator del enemigo

    public AudioClip chaseClip; // Clip de audio cuando el enemigo persigue al jugador
    public AudioClip screamerClip; // Clip de audio para el screamer
    public Canvas screamerCanvas; // Canvas que se muestra durante el screamer
    public Canvas gameOverCanvas; // Canvas de Game Over

    // Referencias a los botones para reiniciar y salir
    public Button restartButton;
    public Button exitButton;

    private bool isChasing = false; // Estado de persecuci�n
    private bool isDead = false; // Estado de muerte del jugador
    private AudioSource audioSource; // Fuente de audio para el chase

    void Start()
    {
        // Si no hay un Animator asignado, intenta obtenerlo autom�ticamente
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // Aseg�rate de que los Canvas est�n desactivados al inicio
        if (screamerCanvas != null)
        {
            screamerCanvas.gameObject.SetActive(false); // Desactiva el canvas del screamer al inicio
        }
        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(false); // Desactiva el canvas de Game Over al inicio
        }

        // Crear un AudioSource para manejar los sonidos
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Asignar los m�todos a los botones
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartScene); // Asignar el bot�n de reinicio
        }
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame); // Asignar el bot�n de salir
        }
    }

    void Update()
    {
        // Si el jugador est� muerto, no se hace nada
        if (isDead)
            return;

        if (isChasing && player != null)
        {
            // Cambia a la animaci�n de correr
            animator.SetBool("isRunning", true);

            // Reproducir el sonido de persecuci�n si no est� sonando
            if (chaseClip != null && !audioSource.isPlaying)
            {
                audioSource.clip = chaseClip;
                audioSource.loop = true; // Lo repetimos mientras persigue
                audioSource.Play();
            }

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

            // Detener el sonido de persecuci�n si el enemigo deja de perseguir
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
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

        // Detener el sonido de persecuci�n cuando el jugador es alcanzado
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Reproducir el clip del screamer
        if (screamerClip != null)
        {
            AudioSource.PlayClipAtPoint(screamerClip, transform.position);
        }

        // Mostrar el Canvas del Screamer
        if (screamerCanvas != null)
        {
            screamerCanvas.gameObject.SetActive(true); // Activar el canvas del screamer cuando se mata al jugador
        }

        // Detener el tiempo del juego
        Time.timeScale = 0;

        // Marcar al jugador como muerto
        isDead = true;

        // Esperar a que termine el screamer (por ejemplo, 2 segundos)
        StartCoroutine(WaitForScreamerToEnd());
    }

    private IEnumerator WaitForScreamerToEnd()
    {
        // Usar WaitForSecondsRealtime para evitar que el Time.timeScale afecte la espera
        yield return new WaitForSecondsRealtime(2f);

        // Desactivar el Canvas del Screamer
        if (screamerCanvas != null)
        {
            screamerCanvas.gameObject.SetActive(false);
        }

        // Activar el Canvas de Game Over
        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(true);

            // Activar el rat�n para la interacci�n
            Cursor.lockState = CursorLockMode.None; // Desbloquear el cursor
            Cursor.visible = true; // Hacer el cursor visible
        }

        // Reanudar el tiempo del juego
        Time.timeScale = 1;
    }

    // M�todo para reiniciar la escena
    public void RestartScene()
    {
        // Desactivar el canvas de screamer antes de reiniciar la escena
        if (screamerCanvas != null)
        {
            screamerCanvas.gameObject.SetActive(false);
        }

        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // M�todo para salir del juego
    public void ExitGame()
    {
        // Salir del juego
        Application.Quit();

        // Si estamos en el editor, detener la ejecuci�n
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Asegurarse de restablecer el estado del cursor cuando el juego se cierra
    private void OnApplicationQuit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
