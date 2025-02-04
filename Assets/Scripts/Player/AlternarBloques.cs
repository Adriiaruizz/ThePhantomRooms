using UnityEngine;

public class AlternarBloques : MonoBehaviour
{
    private bool estadoAzul = true; // Comienza en el estado Azul

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
        string estadoActual = estadoAzul ? "Azul" : "Rojo";

        // Busca todos los objetos con las etiquetas "Azul" y "Rojo"
        GameObject[] bloquesAzules = GameObject.FindGameObjectsWithTag("Azul");
        GameObject[] bloquesRojos = GameObject.FindGameObjectsWithTag("Rojo");

        // Activa/Desactiva según el estado actual
        foreach (GameObject bloque in bloquesAzules)
        {
            bloque.SetActive(estadoAzul);
        }
        foreach (GameObject bloque in bloquesRojos)
        {
            bloque.SetActive(!estadoAzul);
        }

        Debug.Log("Estado cambiado a: " + estadoActual);
    }
}
