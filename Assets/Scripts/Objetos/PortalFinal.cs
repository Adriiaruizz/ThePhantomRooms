using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Necesario para UI

public class PortalFinal : MonoBehaviour
{
    public Canvas felicidadesCanvas; // Canvas de felicitaciones
    public Canvas otroCanvas; // Canvas a desactivar
    public Button salirButton; // Botón para salir del juego

    private void Start()
    {
        if (salirButton != null)
        {
            salirButton.onClick.AddListener(TerminarJuego);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivarFelicitaciones();
        }
    }

    private void ActivarFelicitaciones()
    {
        if (felicidadesCanvas != null)
        {
            felicidadesCanvas.enabled = true; // Activar felicitaciones
        }

        if (otroCanvas != null)
        {
            otroCanvas.enabled = false; // Desactivar otro canvas
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void TerminarJuego()
    {
        Debug.Log("¡Juego terminado!");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
