using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParpadeoLuz : MonoBehaviour
{
    public Light lightSource;
    public float onTime = 0.5f;
    public float offTime = 0.5f;

    public AudioSource audioSource; // Fuente de audio para el sonido

    private Coroutine flickerRoutine;

    void Start()
    {
        // Inicia la rutina de parpadeo
        if (lightSource != null)
        {
            flickerRoutine = StartCoroutine(FlickerLight());
        }
    }

    private IEnumerator FlickerLight()
    {
        while (true)
        {
            // Encender la luz y reproducir sonido
            lightSource.enabled = true;

            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }

            yield return new WaitForSeconds(onTime);

            // Apagar la luz y detener el sonido
            lightSource.enabled = false;

            if (audioSource != null)
            {
                audioSource.Stop();
            }

            yield return new WaitForSeconds(offTime);
        }
    }

    private void OnDisable()
    {
        // Detiene la rutina si el objeto se desactiva
        if (flickerRoutine != null)
        {
            StopCoroutine(flickerRoutine);
        }
    }
}
