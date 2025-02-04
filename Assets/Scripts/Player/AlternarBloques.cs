using UnityEngine;
using System.Collections.Generic;

public class AlternarBloques : MonoBehaviour
{
    private bool estadoAzul = true; // Comienza en el estado Azul
    private List<GameObject> bloquesAzules = new List<GameObject>();
    private List<GameObject> bloquesRojos = new List<GameObject>();

    void Start()
    {
        // Guarda todos los objetos en listas al iniciar el juego
        bloquesAzules.AddRange(GameObject.FindGameObjectsWithTag("Azul"));
        bloquesRojos.AddRange(GameObject.FindGameObjectsWithTag("Rojo"));

        // Aplica el estado inicial
        AlternarEstado();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) // Si el jugador presiona X
        {
            AlternarEstado();
        }
    }

    void AlternarEstado()
    {
        estadoAzul = !estadoAzul; // Cambia entre estados

        // Activa/Desactiva según el estado actual
        foreach (GameObject bloque in bloquesAzules)
        {
            if (bloque != null) bloque.SetActive(estadoAzul);
        }
        foreach (GameObject bloque in bloquesRojos)
        {
            if (bloque != null) bloque.SetActive(!estadoAzul);
        }

        Debug.Log("Estado cambiado a: " + (estadoAzul ? "Azul" : "Rojo"));
    }
}
