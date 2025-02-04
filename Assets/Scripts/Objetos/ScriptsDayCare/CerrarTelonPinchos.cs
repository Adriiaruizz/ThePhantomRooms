using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerrarTelon : MonoBehaviour
{
    public GameObject prefabActivar; // Prefab que se activará cuando el objeto llegue

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjetoMovil")) // Verifica si el objeto que llega tiene el tag "ObjetoMovil"
        {
            if (prefabActivar != null)
            {
                prefabActivar.SetActive(true); // Activa el prefab
                Debug.Log(prefabActivar.name + " ha sido activado por " + other.name);
            }
        }
    }
}
