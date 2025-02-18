using UnityEngine;
using System.Collections;

public class PortalLaberinto : MonoBehaviour
{
    public Transform portalDestino; // Referencia al otro portal
    private bool puedeTeletransportar = true; // Evitar teletransportes dobles

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("tp")) && puedeTeletransportar)
        {
            StartCoroutine(Teletransportar(other));
        }
    }

    private IEnumerator Teletransportar(Collider objeto)
    {
        puedeTeletransportar = false; // Evitar m�ltiples teletransportes

        // Guardar la velocidad si el objeto tiene un Rigidbody
        Rigidbody rb = objeto.GetComponent<Rigidbody>();
        Vector3 velocidadInicial = rb != null ? rb.velocity : Vector3.zero;

        // Desactivar la gravedad temporalmente para evitar que caiga al teletransportarse
        if (rb != null)
        {
            rb.useGravity = false;
        }

        // Desactivar colisi�n moment�neamente para evitar problemas de colisi�n
        Collider objetoCollider = objeto.GetComponent<Collider>();
        Collider portalCollider = GetComponent<Collider>();
        if (objetoCollider != null && portalCollider != null)
        {
            Physics.IgnoreCollision(objetoCollider, portalCollider, true);
        }

        // Teletransportar el objeto
        objeto.transform.position = portalDestino.position + portalDestino.forward * 1.5f;

        // Ajustar la rotaci�n manteniendo 0� en el eje Y
        Quaternion nuevaRotacion = Quaternion.Euler(0, 0, portalDestino.rotation.eulerAngles.z);
        objeto.transform.rotation = nuevaRotacion;

        // Restaurar la velocidad si el objeto tiene Rigidbody
        if (rb != null)
        {
            rb.velocity = portalDestino.forward * velocidadInicial.magnitude;
            rb.useGravity = true; // Restaurar la gravedad
        }

        // Un peque�o retraso para evitar la activaci�n de colisiones inmediatamente
        yield return new WaitForSeconds(0.1f);

        // Reactivar colisi�n con el portal
        if (objetoCollider != null && portalCollider != null)
        {
            Physics.IgnoreCollision(objetoCollider, portalCollider, false);
        }

        puedeTeletransportar = true;
    }
}
