using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<GameObject> objectsToCheck;

    private void Start()
    {
        // Cargar el estado de los objetos al iniciar la escena
        foreach (GameObject obj in objectsToCheck)
        {
            GlobalGameState.LoadObjectState(obj);
        }
    }
}
