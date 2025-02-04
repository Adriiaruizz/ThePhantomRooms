using System.Collections;
using UnityEngine;

public class Pinchos : MonoBehaviour
{
    public GameObject objetoVigilado; // El objeto que, al desactivarse, activará el movimiento
    public float velocidad = 5f; // Velocidad de movimiento en el eje X

    private bool debeMoverse = false;

    void Update()
    {
        // Si el objeto a vigilar se ha desactivado, activar el movimiento
        if (objetoVigilado != null && !objetoVigilado.activeSelf && !debeMoverse)
        {
            debeMoverse = true;
        }

        // Mover el objeto en el eje X si debe moverse
        if (debeMoverse)
        {
            transform.position += -Vector3.right * velocidad * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Si toca al jugador, detiene el juego
        {
            Debug.Log("El objeto ha tocado al jugador. Juego detenido.");
            Time.timeScale = 0f; // Pausar el juego
        }
    }
}
