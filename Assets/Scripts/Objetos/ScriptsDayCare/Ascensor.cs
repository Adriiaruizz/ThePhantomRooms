using System.Collections;
using UnityEngine;

public class Ascensor : MonoBehaviour
{
    public GameObject objetoAMover; // Objeto que se elevará
    public Transform posicionFinal; // Posición objetivo
    public float velocidad = 2f; // Velocidad de elevación
    public GameObject prefabActivar; // Prefab que se activará al llegar
    public MeshRenderer mesh1; // Primer MeshRenderer a desactivar
    public MeshRenderer mesh2; // Segundo MeshRenderer a desactivar

    private bool enMovimiento = false;
    private bool jugadorCerca = false; // Verifica si el jugador está en la zona

    void Update()
    {
        // Verifica si el jugador está cerca y presiona "E"
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            Interactuar();
        }

        if (enMovimiento && objetoAMover != null)
        {
            // Mueve el objeto hacia la posición final
            objetoAMover.transform.position = Vector3.MoveTowards(
                objetoAMover.transform.position,
                posicionFinal.position,
                velocidad * Time.deltaTime
            );

            // Si el objeto llega a la posición final
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
            Debug.Log("Elevación iniciada para " + objetoAMover.name);

            // Desactiva los MeshRenderers si existen
            if (mesh1 != null) mesh1.enabled = false;
            if (mesh2 != null) mesh2.enabled = false;

            // Activa el prefab si está asignado
            if (prefabActivar != null) prefabActivar.SetActive(true);

            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true; // El jugador está cerca
            Debug.Log("Jugador puede interactuar con " + gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false; // El jugador salió de la zona
        }
    }
}
