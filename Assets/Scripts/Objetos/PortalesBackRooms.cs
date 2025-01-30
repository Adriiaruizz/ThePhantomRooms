using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalesBackRooms : MonoBehaviour
{
    public GameObject PortalFlickerRoom; // Objeto que se desactivará si el booleano es true
    public GameObject PortalDarkRoom;
    public GameObject PortalDressingRoom;

    private void Start()
    {
        // Comprueba el valor del booleano y desactiva el objeto si es true
        if (GlobalGameState.FlickerRoomCompleta)
        {
            if (PortalFlickerRoom != null)
            {
                PortalFlickerRoom.SetActive(false);
                Debug.Log($"{PortalFlickerRoom.name} ha sido desactivado porque el jugador entró en el portal.");
            }
            else
            {
                Debug.LogWarning("No se ha asignado un objeto para desactivar en el script PortalesBackRooms.");
            }
        }
        if (GlobalGameState.DarkRoomsCompletas)
        {
            if (PortalDarkRoom != null)
            {
                PortalDarkRoom.SetActive(false);
                Debug.Log($"{PortalDarkRoom.name} ha sido desactivado porque el jugador entró en el portal.");
            }
            else
            {
                Debug.LogWarning("No se ha asignado un objeto para desactivar en el script PortalesBackRooms.");
            }
        }
        if (GlobalGameState.DressingRoomCompleta)
        {
            if (PortalDressingRoom != null)
            {
                PortalDressingRoom.SetActive(false);
                Debug.Log($"{PortalDressingRoom.name} ha sido desactivado porque el jugador entró en el portal.");
            }
            else
            {
                Debug.LogWarning("No se ha asignado un objeto para desactivar en el script PortalesBackRooms.");
            }
        }
    }
}
