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

    // M�todo para comprobar si todas las condiciones est�n cumplidas
    private bool ComprobarCondiciones()
    {
        // Aqu� verificamos si todas las condiciones en GlobalGameState son verdaderas
        if (GlobalGameState.FlickerRoomCompleta &&
            GlobalGameState.DarkRoomsCompletas &&
            GlobalGameState.DressingRoomCompleta &&
            GlobalGameState.DaycareCompleta &&
            GlobalGameState.PuzzleRoom)
        {
            return true; // Las condiciones se cumplen
        }
        return false; // Alguna condici�n no se cumple
    }

    // M�todo para activar los prefabs de la lista
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
