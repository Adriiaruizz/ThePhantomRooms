using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Para cambiar de escena

public class TwistedMuerto : MonoBehaviour
{
    public Transform jugador; // Asignar el jugador desde el Inspector
    private NavMeshAgent agente;

    public AudioSource audioSource; // Fuente de audio
    public AudioClip sonidoJumpscare; // Sonido del jumpscare
    public GameObject canvasJumpscare; // Canvas de jumpscare
    public Image imagenJumpscare; // Imagen de jumpscare
    public float tiempoJumpscare = 1f; // Tiempo que la imagen del jumpscare estará visible
    public GameObject pantallaGameOver; // Pantalla de Game Over

    // Referencias a los botones de la pantalla de Game Over
    public Button botonReiniciar; // Botón de reiniciar
    public Button botonAbandonar; // Botón de abandonar

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();

        if (jugador == null)
        {
            jugador = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Desactivar el Canvas de jumpscare y la pantalla de Game Over al inicio
        if (canvasJumpscare != null)
        {
            canvasJumpscare.SetActive(false);
        }

        if (pantallaGameOver != null)
        {
            pantallaGameOver.SetActive(false);
        }

        // Asignar las funciones a los botones
        if (botonReiniciar != null)
        {
            botonReiniciar.onClick.AddListener(ReiniciarJuego);
        }

        if (botonAbandonar != null)
        {
            botonAbandonar.onClick.AddListener(AbandonarJuego);
        }
    }

    void Update()
    {
        if (jugador != null)
        {
            agente.SetDestination(jugador.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("El jugador ha sido atrapado. Fin del juego.");
            Time.timeScale = 0f; // Detiene el juego

            // Detener el sonido de ambiente si está sonando
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            // Reproducir el sonido del jumpscare
            if (sonidoJumpscare != null)
            {
                audioSource.PlayOneShot(sonidoJumpscare); // Reproducir el sonido
            }

            // Mostrar el jumpscare
            if (canvasJumpscare != null)
            {
                StartCoroutine(MostrarJumpscare());
            }

            // Esperar el tiempo del jumpscare y luego mostrar la pantalla de Game Over
            StartCoroutine(MostrarPantallaGameOver());
        }
    }

    private IEnumerator MostrarJumpscare()
    {
        // Activar el canvas con un pequeño retraso
        yield return new WaitForSecondsRealtime(0.1f);
        if (canvasJumpscare != null)
        {
            canvasJumpscare.SetActive(true); // Activar el canvas con la imagen del jumpscare
        }
    }

    private IEnumerator MostrarPantallaGameOver()
    {
        // Esperar el tiempo del jumpscare sin que Time.timeScale lo afecte
        yield return new WaitForSecondsRealtime(tiempoJumpscare);

        // Restaurar el uso del ratón (habilitar la UI)
        Time.timeScale = 1f; // Restaurar la escala del tiempo

        // Mostrar la pantalla de Game Over
        if (pantallaGameOver != null)
        {
            pantallaGameOver.SetActive(true);
        }

        // Activar la interacción del ratón con la UI (en caso de que estuviera bloqueada)
        Cursor.lockState = CursorLockMode.None; // Liberar el cursor
        Cursor.visible = true; // Hacer visible el cursor
    }

    // Función para reiniciar el juego
    private void ReiniciarJuego()
    {
        // Cambiar la escena actual para reiniciar el juego (usando el nombre de la escena)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // Restaurar la escala del tiempo
    }

    // Función para abandonar el juego
    private void AbandonarJuego()
    {
        // Cerrar la aplicación
        Debug.Log("Juego Abandonado");
        Application.Quit();
    }
}
