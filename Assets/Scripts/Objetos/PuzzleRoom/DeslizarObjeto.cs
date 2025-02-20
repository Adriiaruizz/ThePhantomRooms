using UnityEngine;

public class DeslizarObjeto : MonoBehaviour
{
    public GameObject objetoADeslizar; // Prefab que se moverá
    public Vector3 posicionObjetivo; // Posición final del objeto
    public float velocidad = 2f; // Velocidad del deslizamiento

    public AudioClip sonidoInteraccion; // Sonido al interactuar
    private AudioSource audioSource; // Fuente de audio

    private bool jugadorDentro = false; // Si el jugador está dentro del trigger
    private bool enMovimiento = false; // Si el objeto está en movimiento
    private bool sonidoReproducido = false; // Verifica si el sonido ya se ha reproducido
    private Collider objetoCollider; // Referencia al collider del objeto

    void Start()
    {
        // Obtiene el collider del objeto si tiene uno
        objetoCollider = GetComponent<Collider>();

        // Añadir un AudioSource si no existe
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (enMovimiento && objetoADeslizar != null)
        {
            // Mueve el objeto hacia la posición objetivo
            objetoADeslizar.transform.position = Vector3.MoveTowards(objetoADeslizar.transform.position, posicionObjetivo, velocidad * Time.deltaTime);

            // Si el objeto llega a la posición objetivo, detiene el movimiento
            if (Vector3.Distance(objetoADeslizar.transform.position, posicionObjetivo) < 0.01f)
            {
                enMovimiento = false;
            }
        }

        // Si el jugador está dentro y pulsa "E", activa el movimiento, reproduce el sonido y desactiva el collider
        if (jugadorDentro && Input.GetKeyDown(KeyCode.E) && !sonidoReproducido)
        {
            enMovimiento = true;

            // Reproducir sonido si hay un clip asignado
            if (sonidoInteraccion != null)
            {
                audioSource.PlayOneShot(sonidoInteraccion);
                sonidoReproducido = true; // Evita que se reproduzca nuevamente
            }

            // Desactiva el collider en lugar de desactivar el objeto
            if (objetoCollider != null)
            {
                objetoCollider.enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el jugador ha entrado en el trigger
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true;
            sonidoReproducido = false; // Resetea el sonido para que pueda reproducirse cuando entre en el trigger
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verifica si el jugador ha salido del trigger
        if (other.CompareTag("Player"))
        {
            jugadorDentro = false;
        }
    }
}
