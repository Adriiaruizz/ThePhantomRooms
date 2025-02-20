using UnityEngine;
using TMPro; // Para TextMeshPro
using UnityEngine.UI; // Para UI Text Legacy

public class UI_Mision : MonoBehaviour
{
    public TextMeshProUGUI textoMision; // Para TextMeshPro
    // public Text textoMision; // Usar esta l�nea si usas Text Legacy

    private void Start()
    {
        // Cargar la misi�n almacenada en GlobalGameState
        ActualizarMision(GlobalGameState.MisionActual);
    }

    // M�todo para actualizar la misi�n en la UI y en el estado global
    public void ActualizarMision(string nuevaMision)
    {
        GlobalGameState.ActualizarMision(nuevaMision);
        if (textoMision != null)
        {
            textoMision.text = "Misi�n Actual: " + nuevaMision;
        }
    }
}
