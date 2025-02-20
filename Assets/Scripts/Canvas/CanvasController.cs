using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public Canvas felicidadesCanvas; // Canvas de felicitaciones

    private void Start()
    {
        if (felicidadesCanvas != null)
        {
            felicidadesCanvas.enabled = false; // Asegura que el canvas está desactivado al inicio
        }
    }
}
