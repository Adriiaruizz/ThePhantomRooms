using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TioVivo : MonoBehaviour
{
    public float rotationSpeed = 50f; // Velocidad de giro en grados por segundo

    void Update()
    {
        transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
    }
}
