using UnityEngine;

public class ParedFalsa : MonoBehaviour
{
    public GameObject objetoAElevar; // El objeto que se elevar�
    public Vector3 posicionObjetivo; // Posici�n final a la que se elevar� el objeto
    public float velocidad = 2f; // Velocidad de elevaci�n

    private bool enMovimiento = false; // Controla si el objeto debe moverse

    void OnEnable()
    {
        // Cuando el prefab se activa, inicia la elevaci�n
        enMovimiento = true;
    }

    void Update()
    {
        if (enMovimiento && objetoAElevar != null)
        {
            objetoAElevar.transform.position = Vector3.MoveTowards(objetoAElevar.transform.position, posicionObjetivo, velocidad * Time.deltaTime);

            // Si el objeto llega a la posici�n objetivo, detiene el movimiento
            if (Vector3.Distance(objetoAElevar.transform.position, posicionObjetivo) < 0.01f)
            {
                enMovimiento = false;
            }
        }
    }
}
