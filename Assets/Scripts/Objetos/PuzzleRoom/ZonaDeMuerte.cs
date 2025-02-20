using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZonaDeMuerte : MonoBehaviour
{
    public Canvas gameOverCanvas; // Referencia al Canvas de Game Over
    public Button restartButton; // Botón de reinicio
    public Button exitButton; // Botón de salir

    private void Start()
    {
        // Asegurarse de que el Canvas está desactivado al inicio
        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(false);
        }

        // Asignar funciones a los botones si existen
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartScene);
        }
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el jugador ha entrado en la zona de muerte
        {
            Debug.Log("El jugador ha muerto. Fin del juego.");

            // Mostrar el Canvas de Game Over
            if (gameOverCanvas != null)
            {
                gameOverCanvas.gameObject.SetActive(true);
            }

            // Pausar el tiempo del juego
            Time.timeScale = 0;

            // Habilitar el cursor para que el jugador pueda interactuar con los botones
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void RestartScene()
    {
        // Restaurar el tiempo del juego antes de reiniciar
        Time.timeScale = 1;

        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
