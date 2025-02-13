using UnityEngine;

public class RotarCaja : MonoBehaviour
{
    public float velocidadX = 50f; // Velocidad de rotación en el eje X
    public float velocidadZ = 50f; // Velocidad de rotación en el eje Z

    void Update()
    {
        transform.Rotate(velocidadX * Time.deltaTime, 0, velocidadZ * Time.deltaTime);
    }
}
