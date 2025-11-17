using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;

    private Transform cam;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform; // Pega a câmera principal
    }

    void Update()
    {
        // Entrada do teclado
        float horizontal = Input.GetAxis("Horizontal"); // A/D
        float vertical = Input.GetAxis("Vertical");     // W/S

        // Direção relativa à câmera
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0; // Ignora o eixo Y
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * vertical + right * horizontal;

        controller.Move(move * speed * Time.deltaTime);

        // Gravidade
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Reset da velocidade vertical se estiver no chão
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Pequena força pra manter contato com o chão
        }
    }
}
