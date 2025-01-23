using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform cameraTransform;     // Transform de la c�mara del jugador
    public KeyCode interactKey = KeyCode.E; // Tecla para interactuar
    public float interactDistance = 2.5f; // Distancia m�xima para interactuar

    void Update()
    {
        if (Input.GetKeyDown(interactKey)) // Detectar la tecla de interacci�n
        {
            TryInteract();
            TryInteract2();
        }
    }

    void TryInteract()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward); // Raycast desde la c�mara
        RaycastHit hit;

        // Realiza el raycast para detectar objetos en la distancia de interacci�n
        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            // Intenta obtener el componente Interactable en el objeto alcanzado
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact(); // Llama al m�todo de interacci�n del objeto
            }
        }
    }
    void TryInteract2()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward); // Raycast desde la c�mara
        RaycastHit hit;

        // Realiza el raycast para detectar objetos en la distancia de interacci�n
        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            // Intenta obtener el componente Interactable en el objeto alcanzado
            Interactable2 interactable = hit.collider.GetComponent<Interactable2>();
            if (interactable != null)
            {
                interactable.Interact(); // Llama al m�todo de interacci�n del objeto
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualiza el rango de interacci�n en la escena
        if (cameraTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(cameraTransform.position, cameraTransform.forward * interactDistance);
        }
    }
}
