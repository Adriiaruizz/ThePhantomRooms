using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pinchos : MonoBehaviour
{
    public GameObject objetoVigilado; // El objeto que, al desactivarse, activará el movimiento
    public float velocidad = 5f; // Velocidad de movimiento en el eje X

    private bool debeMoverse = false;

    // Referencias para el Game Over
    public GameObject gameOverCanvas; // Canvas de Game Over
    public Button restartButton; // Botón para reiniciar el juego
    public Button exitButton; // Botón para salir del juego

    void Start()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false); // Asegurarse de que el canvas esté desactivado al principio
        }

        if (restartButton != null) restartButton.onClick.AddListener(RestartGame);
        if (exitButton != null) exitButton.onClick.AddListener(ExitGame);
    }

    void Update()
    {
        // Si el objeto a vigilar se ha desactivado, activar el movimiento
        if (objetoVigilado != null && !objetoVigilado.activeSelf && !debeMoverse)
        {
            debeMoverse = true;
        }

        // Mover el objeto en el eje X si debe moverse
        if (debeMoverse)
        {
            transform.position += -Vector3.right * velocidad * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Si toca al jugador, detiene el juego
        {
            Debug.Log("El jugador ha tocado los pinchos. Juego detenido.");
            GameOver(); // Llamar al método GameOver
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
