using UnityEngine;
using System.Collections;

public class PortalLaberinto : MonoBehaviour
{
    public Transform portalDestino; // Referencia al otro portal
    public AudioClip sonidoPortal; // Sonido al teletransportar

    private bool puedeTeletransportar = true; // Evitar teletransportes dobles
    private AudioSource audioSource; // Fuente de audio

    private void Start()
    {
        // Añadir un AudioSource si no existe
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("tp")) && puedeTeletransportar)
        {
            StartCoroutine(Teletransportar(other));
        }
    }

    private IEnumerator Teletransportar(Collider objeto)
    {
        puedeTeletransportar = false; // Evitar múltiples teletransportes

        // Reproducir sonido si hay un clip asignado
        if (sonidoPortal != null)
        {
            audioSource.PlayOneShot(sonidoPortal);
        }

        // Guardar la velocidad si el objeto tiene un Rigidbody
        Rigidbody rb = objeto.GetComponent<Rigidbody>();
        Vector3 velocidadInicial = rb != null ? rb.velocity : Vector3.zero;

        // Desactivar la gravedad temporalmente para evitar que caiga al teletransportarse
        if (rb != null)
        {
            rb.useGravity = false;
        }

        // Desactivar colisión momentáneamente para evitar problemas de colisión
        Collider objetoCollider = objeto.GetComponent<Collider>();
        Collider portalCollider = GetComponent<Collider>();
        if (objetoCollider != null && portalCollider != null)
        {
            Physics.IgnoreCollision(objetoCollider, portalCollider, true);
        }

        // Teletransportar el objeto
        objeto.transform.position = portalDestino.position + portalDestino.forward * 1.5f;

        // Ajustar la rotación manteniendo 0° en el eje Y
        Quaternion nuevaRotacion = Quaternion.Euler(0, 0, portalDestino.rotation.eulerAngles.z);
        objeto.transform.rotation = nuevaRotacion;

        // Restaurar la velocidad si el objeto tiene Rigidbody
        if (rb != null)
        {
            rb.velocity = portalDestino.forward * velocidadInicial.magnitude;
            rb.useGravity = true; // Restaurar la gravedad
        }

        // Un pequeño retraso para evitar la activación de colisiones inmediatamente
        yield return new WaitForSeconds(0.1f);

        // Reactivar colisión con el portal
        if (objetoCollider != null && portalCollider != null)
        {
            Physics.IgnoreCollision(objetoCollider, portalCollider, false);
        }

        puedeTeletransportar = true;
    }
}
