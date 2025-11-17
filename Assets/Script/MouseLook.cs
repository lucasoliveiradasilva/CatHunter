using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody; // Referência ao corpo do jogador

    private float xRotation = 0f; // Controle da rotação vertical

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Trava cursor
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotação vertical limitada
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f); // Limite: -60° a 60°

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
