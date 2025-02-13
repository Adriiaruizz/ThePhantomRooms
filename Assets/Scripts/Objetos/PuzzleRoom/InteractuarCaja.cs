using UnityEngine;

public class InteractuarCaja : MonoBehaviour
{
    private bool jugadorCerca = false; // Para detectar si el jugador est� cerca
    private ControllerCajaMina controllerCajaMina; // Referencia al script ControllerCajaMina

    void Start()
    {
        // Busca autom�ticamente el ControllerCajaMina en la escena
        controllerCajaMina = FindObjectOfType<ControllerCajaMina>();

        if (controllerCajaMina == null)
        {
            Debug.LogError("No se encontr� un ControllerCajaMina en la escena.");
        }
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E) && controllerCajaMina != null)
        {
            controllerCajaMina.Recogido = true; // Activa el booleano en el otro script
            gameObject.SetActive(false); // Desactiva este objeto
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Aseg�rate de que el jugador tenga la etiqueta "Player"
        {
            jugadorCerca = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }
}
