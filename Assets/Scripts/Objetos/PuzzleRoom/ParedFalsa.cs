using UnityEngine;

public class ParedFalsa : MonoBehaviour
{
    public GameObject objetoAElevar; // El objeto que se elevará
    public Vector3 posicionObjetivo; // Posición final a la que se elevará el objeto
    public float velocidad = 2f; // Velocidad de elevación

    private bool enMovimiento = false; // Controla si el objeto debe moverse

    void OnEnable()
    {
        // Cuando el prefab se activa, inicia la elevación
        enMovimiento = true;
    }

    void Update()
    {
        if (enMovimiento && objetoAElevar != null)
        {
            objetoAElevar.transform.position = Vector3.MoveTowards(objetoAElevar.transform.position, posicionObjetivo, velocidad * Time.deltaTime);

            // Si el objeto llega a la posición objetivo, detiene el movimiento
            if (Vector3.Distance(objetoAElevar.transform.position, posicionObjetivo) < 0.01f)
            {
                enMovimiento = false;
            }
        }
    }
}
