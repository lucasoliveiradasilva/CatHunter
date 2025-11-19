using UnityEngine;

public class CameraShakeAmenti : MonoBehaviour
{
    // ───────────────────────────────────────────────────────────────
    //  ► CONFIGURAÇÕES DO BALANÇO SUAVE (sempre ativo)
    // ───────────────────────────────────────────────────────────────
    [Header("Movimento Base (sempre ativo)")]
    public float positionAmount = 0.05f;    // intensidade do balanço de posição
    public float rotationAmount = 0.8f;     // intensidade do balanço de rotação
    public float positionSpeed = 2f;        // velocidade do balanço suave
    public float rotationSpeed = 3f;        // velocidade da oscilação de rotação

    // ───────────────────────────────────────────────────────────────
    //  ► RUÍDO ALEATÓRIO (estilo Amenti)
    // ───────────────────────────────────────────────────────────────
    [Header("Ruído Procedural (Amenti)")]
    public float noiseFrequency = 1.5f;     // velocidade do ruído
    public float noiseAmount = 0.03f;       // intensidade do ruído

    // ───────────────────────────────────────────────────────────────
    //  ► CAMINHADA (Headbob)
    // ───────────────────────────────────────────────────────────────
    [Header("Movimento de Caminhada")]
    public float walkPositionAmount = 0.05f;    // força da movimentação andando
    public float walkRotationAmount = 1.5f;     // rotação da câmera andando
    public float walkSpeed = 8f;                // frequência do headbob

    [Header("Suavização")]
    public float blendSpeed = 6f;               // suaviza a transição parado ↔ andando

    private float shakeBlend = 0f;              // blend final da caminhada (0 parado, 1 andando)

    // ───────────────────────────────────────────────────────────────
    //  ► REFERÊNCIAS
    // ───────────────────────────────────────────────────────────────
    [Header("Referências")]
    public GameObject player;
    private CharacterController cc;

    // Posição/rotação inicial
    Vector3 startLocalPos;
    Quaternion startLocalRot;

    // ───────────────────────────────────────────────────────────────
    //  ► INICIALIZAÇÃO
    // ───────────────────────────────────────────────────────────────
    void Start()
    {
        startLocalPos = transform.localPosition;
        startLocalRot = transform.localRotation;

        // tenta pegar CharacterController automaticamente
        if (cc == null && player != null)
            cc = player.GetComponent<CharacterController>();
    }

    // ───────────────────────────────────────────────────────────────
    //  ► ATUALIZAÇÃO
    // ───────────────────────────────────────────────────────────────
    void Update()
    {
        float t = Time.time;

        // ----------------------------------------------------------
        //  1) DETECTAR SE ESTÁ ANDANDO
        // ----------------------------------------------------------
        bool isMoving = false;

        if (cc != null)
        {
            isMoving = cc.velocity.magnitude > 0.1f;
        }
        else if (player != null)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
                isMoving = rb.linearVelocity.magnitude > 0.1f;
        }

        float targetBlend = isMoving ? 1f : 0f;
        shakeBlend = Mathf.Lerp(shakeBlend, targetBlend, Time.deltaTime * blendSpeed);

        // ----------------------------------------------------------
        //  2) MOVIMENTO SUAVE DA CÂMERA (sempre ativo)
        // ----------------------------------------------------------
        float px = Mathf.Sin(t * positionSpeed) * positionAmount;
        float py = Mathf.Cos(t * positionSpeed * 0.8f) * positionAmount;

        float rx = Mathf.Sin(t * rotationSpeed) * rotationAmount;
        float ry = Mathf.Cos(t * rotationSpeed * 1.1f) * rotationAmount;

        // ----------------------------------------------------------
        //  3) RUÍDO PROCEDURAL (aleatório, estilo Amenti)
        // ----------------------------------------------------------
        float nx = (Mathf.PerlinNoise(t * noiseFrequency, 0f) - 0.5f) * noiseAmount;
        float ny = (Mathf.PerlinNoise(0f, t * noiseFrequency) - 0.5f) * noiseAmount;

        float nrx = (Mathf.PerlinNoise(t * noiseFrequency, 2f) - 0.5f) * noiseAmount;
        float nry = (Mathf.PerlinNoise(3f, t * noiseFrequency) - 0.5f) * noiseAmount;

        // ----------------------------------------------------------
        //  4) MOVIMENTO DE CAMINHADA (headbob)
        // ----------------------------------------------------------
        float wbX = Mathf.Sin(t * walkSpeed) * walkPositionAmount * shakeBlend;
        float wbY = Mathf.Cos(t * walkSpeed * 2f) * walkPositionAmount * shakeBlend;

        float wrX = Mathf.Sin(t * walkSpeed) * walkRotationAmount * shakeBlend;
        float wrY = Mathf.Cos(t * walkSpeed * 1.3f) * walkRotationAmount * shakeBlend;

        // ----------------------------------------------------------
        //  5) APLICAR POSIÇÃO FINAL
        // ----------------------------------------------------------
        transform.localPosition =
            startLocalPos +
            new Vector3(
                px + nx + wbX,
                py + ny + wbY,
                0f
            );

        // ----------------------------------------------------------
        //  6) APLICAR ROTAÇÃO FINAL
        // ----------------------------------------------------------
        transform.localRotation = Quaternion.Euler(
            startLocalRot.eulerAngles.x + rx + nrx + wrX,
            startLocalRot.eulerAngles.y + ry + nry + wrY,
            startLocalRot.eulerAngles.z
        );
    }
}