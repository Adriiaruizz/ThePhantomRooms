using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float speed = 5f;           // Velocidad de movimiento normal
    public float sprintSpeed = 8f;     // Velocidad al correr
    public float jumpForce = 5f;       // Fuerza del salto
    public float sensitivity = 2f;     // Sensibilidad de la cámara

    private Rigidbody rb;              // Referencia al Rigidbody
    private Transform cameraTransform;
    private float rotationX = 0f;
    private bool isGrounded = true;    // Verifica si el jugador está en el suelo
    private float currentSpeed;        // Velocidad actual del jugador

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Evita que el Rigidbody afecte la rotación
        cameraTransform = GetComponentInChildren<Camera>().transform;
        Cursor.lockState = CursorLockMode.Locked; // Oculta y bloquea el cursor
        currentSpeed = speed; // Inicializa la velocidad actual
    }

    void Update()
    {
        HandleMovement();
        HandleCameraRotation();
        HandleJump();
    }

    void HandleMovement()
    {
        // Detecta si el jugador está corriendo
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = speed;
        }

        // Movimiento con WASD
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = (transform.forward * moveVertical + transform.right * moveHorizontal) * currentSpeed;

        // Aplica movimiento solo en los ejes X y Z
        Vector3 velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        rb.velocity = velocity;
    }

    void HandleCameraRotation()
    {
        // Rotación del jugador y la cámara
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleJump()
    {
        // Saltar con la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Detectar si el jugador está en el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
