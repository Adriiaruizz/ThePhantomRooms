using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarParedes : MonoBehaviour
{
    public List<GameObject> objetosADesactivar; // Lista de objetos a desactivar
    private bool jugadorCerca = false; // Indica si el jugador está cerca

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true; // El jugador está dentro del área de interacción
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false; // El jugador salió del área de interacción
        }
    }

    private void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E)) // Si el jugador está cerca y presiona "E"
        {
            DesactivarObjetos();
        }
    }

    private void DesactivarObjetos()
    {
        foreach (GameObject obj in objetosADesactivar)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        gameObject.SetActive(false); // Desactiva el objeto con este script
        Debug.Log("Objetos desactivados por " + gameObject.name);
    }
}
