using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform cameraTransform;     // Transform de la cámara del jugador
    public KeyCode interactKey = KeyCode.E; // Tecla para interactuar
    public float interactDistance = 2.5f; // Distancia máxima para interactuar

    void Update()
    {
        if (Input.GetKeyDown(interactKey)) // Detectar la tecla de interacción
        {
            TryInteract();
            TryInteract2();
        }
    }

    void TryInteract()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward); // Raycast desde la cámara
        RaycastHit hit;

        // Realiza el raycast para detectar objetos en la distancia de interacción
        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            // Intenta obtener el componente Interactable en el objeto alcanzado
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact(); // Llama al método de interacción del objeto
            }
        }
    }
    void TryInteract2()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward); // Raycast desde la cámara
        RaycastHit hit;

        // Realiza el raycast para detectar objetos en la distancia de interacción
        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            // Intenta obtener el componente Interactable en el objeto alcanzado
            Interactable2 interactable = hit.collider.GetComponent<Interactable2>();
            if (interactable != null)
            {
                interactable.Interact(); // Llama al método de interacción del objeto
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualiza el rango de interacción en la escena
        if (cameraTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(cameraTransform.position, cameraTransform.forward * interactDistance);
        }
    }
}
