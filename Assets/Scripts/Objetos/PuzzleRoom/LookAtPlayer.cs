using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("No se encontr� ning�n objeto con la etiqueta 'Player'");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Gira la c�mara para mirar al jugador
            transform.LookAt(player);

            // Corrige la rotaci�n para que la lente de la c�mara mire correctamente
            transform.Rotate(0, 75, 0);
        }
    }
}
