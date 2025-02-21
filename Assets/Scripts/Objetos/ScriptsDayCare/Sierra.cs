using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sierra : MonoBehaviour
{
    public Transform puntoBajada; // Posición a la que bajará la sierra
    public float velocidad = 3f;  // Velocidad de movimiento
    public float esperaTiempo = 2f; // Tiempo de espera en cada posición

    private Vector3 posicionInicial;
    private bool enMovimiento = true;
    private AudioSource audioSource; // Referencia al AudioSource

    public AudioClip sonidoBajada; // Sonido que se reproducirá al llegar al punto de bajada
    public GameObject gameOverCanvas; // Referencia al Canvas de Game Over
    public Button restartButton; // Botón para reiniciar el juego
    public Button exitButton; // Botón para salir del juego

    void Start()
    {
        posicionInicial = transform.position;
        audioSource = GetComponent<AudioSource>(); // Obtener el AudioSource

        if (audioSource == null)
        {
            Debug.LogError("No se encontró un AudioSource en " + gameObject.name);
        }

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false); // Ocultar el Canvas al inicio
        }

        if (restartButton != null) restartButton.onClick.AddListener(RestartGame);
        if (exitButton != null) exitButton.onClick.AddListener(ExitGame);

        StartCoroutine(MoverSierra());
    }

    IEnumerator MoverSierra()
    {
        while (enMovimiento)
        {
            // Mover hacia abajo
            yield return MoverA(puntoBajada.position);

            // Reproducir el sonido al llegar al punto de bajada
            if (audioSource != null && sonidoBajada != null)
            {
                audioSource.PlayOneShot(sonidoBajada);
            }

            yield return new WaitForSeconds(esperaTiempo);

            // Mover hacia arriba
            yield return MoverA(posicionInicial);
            yield return new WaitForSeconds(esperaTiempo);
        }
    }

    IEnumerator MoverA(Vector3 destino)
    {
        while (Vector3.Distance(transform.position, destino) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("El jugador ha tocado la sierra. Juego detenido.");
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f; // Pausar el juego
        if (gameOverCanvas != null) gameOverCanvas.SetActive(true);

        // Activar el cursor para que se pueda usar el ratón
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Restaurar el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }
}
