using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirarBiberon : MonoBehaviour
{
    public float velocidadRotacion = 50f; // Velocidad de rotación en grados por segundo

    void Update()
    {
        transform.Rotate(0, 0, velocidadRotacion * Time.deltaTime);
    }
}

