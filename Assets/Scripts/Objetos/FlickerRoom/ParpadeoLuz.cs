using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParpadeoLuz : MonoBehaviour
{
    public Light lightSource;          
    public float onTime = 0.5f;        
    public float offTime = 0.5f;       

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
            // Encender la luz
            lightSource.enabled = true;
            yield return new WaitForSeconds(onTime);

            // Apagar la luz
            lightSource.enabled = false;
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
