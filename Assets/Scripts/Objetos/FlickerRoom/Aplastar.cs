using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Importar librer�a para manejar botones

public class Aplastar : MonoBehaviour
{
    public Transform positionA; // Posici�n inicial (A)
    public Transform positionB; // Posici�n final (B)
    public float movementSpeed = 5f; // Velocidad de movimiento
    public float waitTimeAtA = 2f; // Tiempo de espera en posici�n A
    public float waitTimeAtB = 2f; // Tiempo de espera en posici�n B

    public AudioSource impactSound; // Sonido al llegar a la posici�n B
    public Canvas gameOverCanvas; // Canvas de fin de juego

    // Botones del Canvas
    public Button restartButton;
    public Button exitButton;

    private Coroutine currentRoutine;

    private void Start()
    {
        // Asegurar que el canvas de Game Over est� desactivado al inicio
        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(false); // Desactivamos el Canvas al inicio
        }

        // Asignar funciones a los botones
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }

        // Comienza el ciclo de ca�da y subida
        currentRoutine = StartCoroutine(FallAndRiseCycle());
    }

    private IEnumerator FallAndRiseCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTimeAtA);
            yield return StartCoroutine(MoveToPosition(positionB.position));

            if (impactSound != null)
            {
                impactSound.Play();
            }

            yield return new WaitForSeconds(waitTimeAtB);
            yield return StartCoroutine(MoveToPosition(positionA.position));
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / movementSpeed;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    private void OnDisable()
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("El jugador ha sido aplastado. Fin del juego.");

            Time.timeScale = 0;

            // Asegurarse de que el canvas de Game Over se active
            if (gameOverCanvas != null)
            {
                gameOverCanvas.gameObject.SetActive(true); // Activar el Canvas
            }
            else
            {
                Debug.LogError("Game Over Canvas no asignado en el Inspector.");
            }

            // Habilitar el cursor para que el jugador pueda interactuar con el men�
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // M�todo para el bot�n "Reintentar"
    public void RestartGame()
    {
        Time.timeScale = 1; // Restablecer el tiempo antes de reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recargar la escena actual
    }

    // M�todo para el bot�n "Salir"
    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Cerrar la aplicaci�n
    }
}
