using System.Collections;
using UnityEngine;

public class Ascensor : MonoBehaviour
{
    public GameObject objetoAMover; // Objeto que se elevar�
    public Transform posicionFinal; // Posici�n objetivo
    public float velocidad = 2f; // Velocidad de elevaci�n
    public GameObject prefabActivar; // Prefab que se activar� al llegar
    public MeshRenderer mesh1; // Primer MeshRenderer a desactivar
    public MeshRenderer mesh2; // Segundo MeshRenderer a desactivar

    private bool enMovimiento = false;
    private bool jugadorCerca = false; // Verifica si el jugador est� en la zona

    void Update()
    {
        // Verifica si el jugador est� cerca y presiona "E"
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            Interactuar();
        }

        if (enMovimiento && objetoAMover != null)
        {
            // Mueve el objeto hacia la posici�n final
            objetoAMover.transform.position = Vector3.MoveTowards(
                objetoAMover.transform.position,
                posicionFinal.position,
                velocidad * Time.deltaTime
            );

            // Si el objeto llega a la posici�n final
            if (Vector3.Distance(objetoAMover.transform.position, posicionFinal.position) < 0.01f)
            {
                enMovimiento = false;
            }
        }
    }

    public void Interactuar()
    {
        if (!enMovimiento)
        {
            enMovimiento = true;
            Debug.Log("Elevaci�n iniciada para " + objetoAMover.name);

            // Desactiva los MeshRenderers si existen
            if (mesh1 != null) mesh1.enabled = false;
            if (mesh2 != null) mesh2.enabled = false;

            // Activa el prefab si est� asignado
            if (prefabActivar != null) prefabActivar.SetActive(true);

            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true; // El jugador est� cerca
            Debug.Log("Jugador puede interactuar con " + gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false; // El jugador sali� de la zona
        }
    }
}
