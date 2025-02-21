using UnityEngine;

public class SonidoBucle : MonoBehaviour
{
    private AudioSource audioSource; // Referencia al AudioSource

    void Start()
    {
        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No se encontró un AudioSource en " + gameObject.name);
            return;
        }

        // Asegurar que el audio se reproduce en bucle
        audioSource.loop = true;

        // Reproducir el audio si no está sonando
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
