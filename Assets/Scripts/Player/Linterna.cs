using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    public Light lanternLight;   // Referencia a la luz de la linterna
    public KeyCode toggleKey = KeyCode.F; // Tecla para encender y apagar (por defecto 'F')

    void Start()
    {
        // Asegúrate de que la luz esté apagada al inicio
        if (lanternLight != null)
        {
            lanternLight.enabled = false;
        }
    }

    void Update()
    {
        // Verifica si el jugador presiona la tecla para encender o apagar la linterna
        if (Input.GetKeyDown(toggleKey))
        {
            if (lanternLight != null)
            {
                lanternLight.enabled = !lanternLight.enabled; // Cambia el estado de la luz
            }
        }
    }
}
