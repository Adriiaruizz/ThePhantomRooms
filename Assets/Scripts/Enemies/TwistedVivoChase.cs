using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwistedVivoChase : MonoBehaviour
{
    public Transform jugador;
    private NavMeshAgent agente;
    public float distanciaMuerte = 1.5f;

    public AudioSource audioSource;
    public AudioClip sonidoAmbiente;
    public AudioClip sonidoJumpscare;
    private AudioSource audioSourceJumpscare;

    public GameObject canvasJumpscare;
    public float tiempoJumpscare = 1f;

    public GameObject pantallaGameOver;
    public Button botonReiniciar;
    public Button botonAbandonar;

    void Start()
    {
        // Configuración inicial
        agente = GetComponent<NavMeshAgent>();

        if (jugador == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                jugador = playerObject.transform;
            }
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (audioSourceJumpscare == null)
        {
            audioSourceJumpscare = gameObject.AddComponent<AudioSource>();
        }

        // Configurar el AudioSource
        ConfigurarAudioSource(audioSource);
        ConfigurarAudioSource(audioSourceJumpscare, false);

        if (sonidoAmbiente != null)
        {
            audioSource.clip = sonidoAmbiente;
            audioSource.loop = true;
            audioSource.Play();
        }

        if (canvasJumpscare != null)
        {
            canvasJumpscare.SetActive(false);
        }

        if (pantallaGameOver != null)
        {
            pantallaGameOver.SetActive(false);
        }

        if (botonReiniciar != null)
        {
            botonReiniciar.onClick.AddListener(RestartGame);
        }

        if (botonAbandonar != null)
        {
            botonAbandonar.onClick.AddListener(ExitGame);
        }
    }

    void Update()
    {
        // Si el jugador está asignado, persíguelo
        if (jugador != null)
        {
            agente.SetDestination(jugador.position);

            if (Vector3.Distance(transform.position, jugador.position) <= distanciaMuerte)
            {
                MatarJugador();
            }
        }
    }

    void MatarJugador()
    {
        Debug.Log("¡El jugador ha muerto!");
        Time.timeScale = 0f; // Detener el tiempo

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (sonidoJumpscare != null && canvasJumpscare != null)
        {
            StartCoroutine(MostrarJumpscareYReproducirSonido());
        }

        StartCoroutine(MostrarPantallaGameOver());
    }

    private IEnumerator MostrarJumpscareYReproducirSonido()
    {
        if (canvasJumpscare != null)
        {
            canvasJumpscare.SetActive(true);
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (sonidoJumpscare != null)
        {
            audioSourceJumpscare.PlayOneShot(sonidoJumpscare);
        }

        yield return new WaitForSecondsRealtime(tiempoJumpscare);
    }

    private IEnumerator MostrarPantallaGameOver()
    {
        yield return new WaitForSecondsRealtime(tiempoJumpscare);

        if (pantallaGameOver != null)
        {
            pantallaGameOver.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void RestartGame()
    {
        // Asegurarse de que el tiempo se reinicie antes de recargar la escena
        Time.timeScale = 1f;

        // Recargar la escena actual de inmediato
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Esperar un poco para que todo se recargue correctamente
        StartCoroutine(EsperarYReiniciarTiempo());
    }

    private IEnumerator EsperarYReiniciarTiempo()
    {
        // Esperamos un cuadro para asegurar que la escena se recargue completamente
        yield return null;

        // Reiniciar el sonido y otros elementos después de la recarga de la escena
        if (audioSource != null && sonidoAmbiente != null)
        {
            audioSource.Play();
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void ConfigurarAudioSource(AudioSource source, bool loop = true)
    {
        source.playOnAwake = false;
        source.loop = loop;
        source.volume = 1f;
    }
}
