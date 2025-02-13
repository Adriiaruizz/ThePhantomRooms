using UnityEngine;

public class ActivarCajaMina : MonoBehaviour
{
    public GameObject prefabActivar; // Prefab que se activar�
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
            if (controllerCajaMina.Recogido) // Verifica si el booleano es true
            {
                if (prefabActivar != null)
                {
                    prefabActivar.SetActive(true); // Activa el prefab
                }
            }
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
