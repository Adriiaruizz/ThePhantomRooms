using UnityEngine;

public class RotarCheese : MonoBehaviour
{
    public float velocidadRotacion = 50f; // Velocidad de rotación, ajustable en el Inspector

    void Update()
    {
        // Rota el objeto sobre su eje Y
        transform.Rotate(0, 0, velocidadRotacion * Time.deltaTime);
    }
}
