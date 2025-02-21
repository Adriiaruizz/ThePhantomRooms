using UnityEngine;

public class DesactivarCanvasConCollider : MonoBehaviour
{
    public GameObject canvas; // Referencia al Canvas que se va a desactivar

    // Desactivar el Canvas cuando otro objeto sale del área del Collider
    private void OnTriggerExit(Collider other)
    {
        if (canvas != null)
        {
            canvas.SetActive(false); // Desactivamos el Canvas cuando el objeto sale
        }
        else
        {
            Debug.LogError("No se ha asignado un Canvas.");
        }
    }
}
