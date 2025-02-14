using UnityEngine;

public class AscensorBasura : MonoBehaviour
{
    public MeshRenderer mesh1; // Primer MeshRenderer a desactivar
    public MeshRenderer mesh2; // Segundo MeshRenderer a desactivar

    public GameObject objetoAMover; // Prefab que bajará
    public Transform destino; // Posición final del movimiento
    public float velocidad = 2f; // Velocidad de movimiento

    public GameObject prefabADesactivar; // Prefab que se desactivará cuando el objeto termine de moverse

    private bool jugadorDentro = false;
    private bool enMovimiento = false;

    void Update()
    {
        // Si el jugador está dentro y pulsa "E"
        if (jugadorDentro && Input.GetKeyDown(KeyCode.E))
        {
            // Desactiva los MeshRenderers
            if (mesh1 != null) mesh1.enabled = false;
            if (mesh2 != null) mesh2.enabled = false;

            // Activa el movimiento del objeto
            enMovimiento = true;
        }

        // Mueve el objeto hasta el destino
        if (enMovimiento && objetoAMover != null)
        {
            objetoAMover.transform.position = Vector3.MoveTowards(objetoAMover.transform.position, destino.position, velocidad * Time.deltaTime);

            // Si llega al destino, desactiva el tercer prefab
            if (Vector3.Distance(objetoAMover.transform.position, destino.position) < 0.01f)
            {
                enMovimiento = false;
                if (prefabADesactivar != null)
                {
                    prefabADesactivar.SetActive(false);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = false;
        }
    }
}
