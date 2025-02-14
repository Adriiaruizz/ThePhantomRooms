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
            Debug.LogWarning("No se encontró ningún objeto con la etiqueta 'Player'");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Gira la cámara para mirar al jugador
            transform.LookAt(player);

            // Corrige la rotación para que la lente de la cámara mire correctamente
            transform.Rotate(0, 75, 0);
        }
    }
}
