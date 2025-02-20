using UnityEngine;
using TMPro; // Para TextMeshPro
using UnityEngine.UI; // Para UI Text Legacy

public class UI_Mision : MonoBehaviour
{
    public TextMeshProUGUI textoMision; // Para TextMeshPro
    // public Text textoMision; // Usar esta línea si usas Text Legacy

    private void Start()
    {
        // Cargar la misión almacenada en GlobalGameState
        ActualizarMision(GlobalGameState.MisionActual);
    }

    // Método para actualizar la misión en la UI y en el estado global
    public void ActualizarMision(string nuevaMision)
    {
        GlobalGameState.ActualizarMision(nuevaMision);
        if (textoMision != null)
        {
            textoMision.text = "Misión Actual: " + nuevaMision;
        }
    }
}
