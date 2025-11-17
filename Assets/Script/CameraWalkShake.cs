using UnityEngine;

public class CameraWalkShake : MonoBehaviour
{
    [Header("Referências")]
    public Transform player; // usado para ler a velocidade do jogador (opcional)
    public CharacterController cc; // se usar CharacterController

    [Header("Intensidade do movimento ao andar")]
    public float walkPositionAmount = 0.05f;
    public float walkRotationAmount = 0.5f;

    [Header("Velocidade do movimento ao andar")]
    public float walkSpeed = 9f;

    [Header("Suavização (ligar/desligar shake)")]
    public float blendSpeed = 5f; // suaviza transição entre parado e andando

    [Header("Ruído opcional (efeito Amenti)")]
    public float noiseAmount = 0.02f;
    public float noiseFrequency = 1.5f;

    Vector3 startLocalPos;
    Quaternion startLocalRot;

    float shakeBlend = 0f;

    void Start()
    {
        startLocalPos = transform.localPosition;
        startLocalRot = transform.localRotation;

        if (cc == null && player != null)
            cc = player.GetComponent<CharacterController>();
    }

    void Update()
    {
        bool isMoving = false;

        // Detecta movimento
        if (cc != null)
        {
            isMoving = cc.velocity.magnitude > 0.1f;
        }
        else if (player != null)
        {
            // fallback se não usar CharacterController
            isMoving = player.GetComponent<Rigidbody>()?.velocity.magnitude > 0.1f;
        }

        // Blend (0 parado → 1 andando)
        float targetBlend = isMoving ? 1f : 0f;
        shakeBlend = Mathf.Lerp(shakeBlend, targetBlend, Time.deltaTime * blendSpeed);

        float t = Time.time * walkSpeed;

        // Head bob só quando andando
        float bx = Mathf.Sin(t) * walkPositionAmount * shakeBlend;
        float by = Mathf.Cos(t * 2f) * walkPositionAmount * shakeBlend;

        float rx = Mathf.Sin(t) * walkRotationAmount * shakeBlend;
        float ry = Mathf.Cos(t * 1.3f) * walkRotationAmount * shakeBlend;

        // Ruído estilo Amenti
        float nx = (Mathf.PerlinNoise(t * noiseFrequency, 0f) - 0.5f) * noiseAmount * shakeBlend;
        float ny = (Mathf.PerlinNoise(0f, t * noiseFrequency) - 0.5f) * noiseAmount * shakeBlend;

        // Aplicar posição
        transform.localPosition = startLocalPos + new Vector3(bx + nx, by + ny, 0f);

        // Aplicar rotação
        transform.localRotation = Quaternion.Euler(
            startLocalRot.eulerAngles.x + rx,
            startLocalRot.eulerAngles.y + ry,
            startLocalRot.eulerAngles.z
        );
    }
}
