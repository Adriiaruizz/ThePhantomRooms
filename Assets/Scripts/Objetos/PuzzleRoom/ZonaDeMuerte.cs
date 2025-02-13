using UnityEngine;

public class ZonaDeMuerte : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el jugador ha entrado en el collider
        {
            Debug.Log("El jugador ha muerto. Fin del juego.");
            Time.timeScale = 0; // Detiene el juego
        }
    }
}
