using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TwistedVivoChase : MonoBehaviour
{
    public Transform jugador; // Referencia al jugador
    private NavMeshAgent agente; // Referencia al NavMeshAgent
    public float distanciaMuerte = 1.5f; // Distancia para matar al jugador

    void Start()
    {
        // Obtiene el NavMeshAgent del objeto
        agente = GetComponent<NavMeshAgent>();

        // Encuentra al jugador si no se ha asignado manualmente
        if (jugador == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                jugador = playerObject.transform;
            }
        }
    }

    void Update()
    {
        // Si el jugador está asignado, persíguelo
        if (jugador != null)
        {
            agente.SetDestination(jugador.position);

            // Comprobar si el enemigo ha alcanzado al jugador
            if (Vector3.Distance(transform.position, jugador.position) <= distanciaMuerte)
            {
                MatarJugador();
            }
        }
    }

    void MatarJugador()
    {
        Debug.Log("¡El jugador ha muerto!");
        Time.timeScale = 0f; // Detiene el juego
    }
}
