using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalFinal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TerminarJuego();
        }
    }

    private void TerminarJuego()
    {
        Debug.Log("�Juego terminado!");
        Application.Quit(); // Cierra el juego en una build

        // Si est�s en el editor de Unity, usa esta l�nea para detener el juego:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
