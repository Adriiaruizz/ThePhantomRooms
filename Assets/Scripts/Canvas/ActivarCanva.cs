using UnityEngine;

public class ActivarCanvasConCollider : MonoBehaviour
{
    public GameObject canvas; // Referencia al Canvas que se va a activar

    // Activar el Canvas cuando otro objeto entra en el área del Collider
    private void OnTriggerEnter(Collider other)
    {
        if (canvas != null)
        {
            canvas.SetActive(true); // Activamos el Canvas cuando el objeto entra
        }
        else
        {
            Debug.LogError("No se ha asignado un Canvas.");
        }
    }
}
