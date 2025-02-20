using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalGameState
{
    // Diccionario para almacenar el estado de activaci�n/desactivaci�n de los objetos
    public static Dictionary<string, bool> objectStates = new Dictionary<string, bool>();

    // Estado de las habitaciones completadas
    public static bool FlickerRoomCompleta = false;
    public static bool DarkRoomsCompletas = false;
    public static bool DressingRoomCompleta = false;
    public static bool DaycareCompleta = false;
    public static bool PuzzleRoom = false;

    // Misi�n actual
    public static string MisionActual = "Activa el generador"; // Misi�n inicial por defecto

    // M�todo para actualizar la misi�n
    public static void ActualizarMision(string nuevaMision)
    {
        MisionActual = nuevaMision;
    }

    // M�todo para guardar el estado de un objeto
    public static void SaveObjectState(GameObject obj)
    {
        if (obj != null)
        {
            objectStates[obj.name] = obj.activeSelf;
        }
    }

    // M�todo para cargar el estado de un objeto
    public static void LoadObjectState(GameObject obj)
    {
        if (obj != null && objectStates.ContainsKey(obj.name))
        {
            obj.SetActive(objectStates[obj.name]);
        }
    }
}
