using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerFin : MonoBehaviour
{
    public List<GameObject> prefabsAActivar; // Lista de prefabs a activar
 

    void Update()
    {
        // Verificar si se cumplen todas las condiciones
        if (ComprobarCondiciones())
        {
            // Si las condiciones se cumplen, activar los prefabs
            ActivarPrefabs();
        }
    }

    // Método para comprobar si todas las condiciones están cumplidas
    private bool ComprobarCondiciones()
    {
        // Aquí verificamos si todas las condiciones en GlobalGameState son verdaderas
        if (GlobalGameState.FlickerRoomCompleta &&
            GlobalGameState.DarkRoomsCompletas &&
            GlobalGameState.DressingRoomCompleta &&
            GlobalGameState.DaycareCompleta &&
            GlobalGameState.PuzzleRoom)
        {
            return true; // Las condiciones se cumplen
        }
        return false; // Alguna condición no se cumple
    }

    // Método para activar los prefabs de la lista
    private void ActivarPrefabs()
    {
        foreach (GameObject prefab in prefabsAActivar)
        {
            if (prefab != null)
            {
                prefab.SetActive(true); // Activar el prefab
                
            }
        }
    }
}
