using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalGameState
{
    // Diccionario para almacenar el estado de activaci�n/desactivaci�n de los objetos
    public static Dictionary<string, bool> objectStates = new Dictionary<string, bool>();

    // Booleano global que indica si el jugador ha entrado al portal
    public static bool FlickerRoomCompleta = false;

    // M�todo para guardar el estado de un objeto
    public static void SaveObjectState(GameObject obj)
    {
        if (obj != null)
        {
            // Guarda el estado del objeto (activado o desactivado)
            objectStates[obj.name] = obj.activeSelf;
        }
    }

    // M�todo para cargar el estado de un objeto
    public static void LoadObjectState(GameObject obj)
    {
        if (obj != null && objectStates.ContainsKey(obj.name))
        {
            // Restaura el estado del objeto
            obj.SetActive(objectStates[obj.name]);
        }
    }
}
