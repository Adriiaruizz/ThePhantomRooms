using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Aplastar : MonoBehaviour
{
    public Transform positionA; // Posici�n inicial (A)
    public Transform positionB; // Posici�n final (B)
    public float movementSpeed = 5f; // Velocidad de movimiento
    public float waitTimeAtA = 2f; // Tiempo de espera en posici�n A
    public float waitTimeAtB = 2f; // Tiempo de espera en posici�n B

    private Coroutine currentRoutine;

    private void Start()
    {
        // Comienza el ciclo de ca�da y subida
        currentRoutine = StartCoroutine(FallAndRiseCycle());
    }

    private IEnumerator FallAndRiseCycle()
    {
        while (true)
        {
            // Esperar en la posici�n A
            yield return new WaitForSeconds(waitTimeAtA);

            // Mover hacia la posici�n B
            yield return StartCoroutine(MoveToPosition(positionB.position));

            // Esperar en la posici�n B
            yield return new WaitForSeconds(waitTimeAtB);

            // Mover de regreso a la posici�n A
            yield return StartCoroutine(MoveToPosition(positionA.position));
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / movementSpeed; // Calcula la duraci�n en funci�n de la velocidad

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Asegurarse de llegar exactamente al destino
    }

    private void OnDisable()
    {
        // Detiene la rutina si el objeto se desactiva
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisiona es el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("El jugador ha sido aplastado. Fin del juego.");
            Time.timeScale = 0; // Detener el tiempo (pausar el juego)
        }
    }
}
