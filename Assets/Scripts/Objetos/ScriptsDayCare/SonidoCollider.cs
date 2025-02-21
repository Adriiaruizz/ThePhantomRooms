using UnityEngine;

public class ReproducirSonidoConCollider : MonoBehaviour
{
    public AudioSource audioSource; // Referencia al AudioSource
    public AudioClip sonido; // Referencia al sonido que se va a reproducir

    // Llamado cuando otro objeto entra en el área del Collider
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en el collider es el jugador
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga la etiqueta "Player"
        {
            if (audioSource != null && sonido != null)
            {
                audioSource.PlayOneShot(sonido); // Reproduce el sonido
            }
            else
            {
                Debug.LogError("Faltan referencias: AudioSource o AudioClip no asignados.");
            }
        }
    }
}
