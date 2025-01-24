using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalesBackRooms : MonoBehaviour
{
    public GameObject objectToDisable; // Objeto que se desactivará si el booleano es true

    private void Start()
    {
        // Comprueba el valor del booleano y desactiva el objeto si es true
        if (GlobalGameState.FlickerRoomCompleta)
        {
            if (objectToDisable != null)
            {
                objectToDisable.SetActive(false);
                Debug.Log($"{objectToDisable.name} ha sido desactivado porque el jugador entró en el portal.");
            }
            else
            {
                Debug.LogWarning("No se ha asignado un objeto para desactivar en el script PortalesBackRooms.");
            }
        }
    }
}
