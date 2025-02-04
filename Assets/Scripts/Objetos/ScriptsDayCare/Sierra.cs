using System.Collections;
using UnityEngine;

public class Sierra : MonoBehaviour
{
    public Transform puntoBajada; // Posición a la que bajará la sierra
    public float velocidad = 3f;  // Velocidad de movimiento
    public float esperaTiempo = 2f; // Tiempo de espera en cada posición

    private Vector3 posicionInicial;
    private bool enMovimiento = true;

    void Start()
    {
        posicionInicial = transform.position;
        StartCoroutine(MoverSierra());
    }

    IEnumerator MoverSierra()
    {
        while (enMovimiento)
        {
            // Mover hacia abajo
            yield return MoverA(puntoBajada.position);
            yield return new WaitForSeconds(esperaTiempo);

            // Mover hacia arriba
            yield return MoverA(posicionInicial);
            yield return new WaitForSeconds(esperaTiempo);
        }
    }

    IEnumerator MoverA(Vector3 destino)
    {
        while (Vector3.Distance(transform.position, destino) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("El jugador ha tocado la sierra. Juego detenido.");
            Time.timeScale = 0f; // Pausa el juego
        }
    }
}
