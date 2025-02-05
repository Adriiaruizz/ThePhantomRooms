using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlladorGeneradores : MonoBehaviour
{
    public static ControlladorGeneradores Instance;
    private int activatedObjects = 0;
    public int totalObjects = 3;
    public GameObject[] objectsToDisable; // Lista de objetos a desactivar
    public GameObject objetoDesactivar;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ObjectActivated()
    {
        activatedObjects++;

        if (activatedObjects >= totalObjects)
        {
            DisableObjects();
        }
    }

    void DisableObjects()
    {
        objetoDesactivar.SetActive(false);
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
            
        }
    }
}
