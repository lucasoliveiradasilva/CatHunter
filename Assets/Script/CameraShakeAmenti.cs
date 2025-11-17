using UnityEngine;

public class CameraShakeAmenti : MonoBehaviour
{
    [Header("Intensidade do movimento")]
    public float positionAmount = 0.05f; // quanto a câmera balança para os lados
    public float rotationAmount = 0.8f;  // quanto a câmera gira levemente

    [Header("Velocidade do movimento")]
    public float positionSpeed = 2f;     // velocidade do balanço de posição
    public float rotationSpeed = 3f;     // velocidade das rotações

    [Header("Frequência aleatória")]
    public float noiseFrequency = 1.5f;  // adiciona ruído procedural estilo “caos leve”

    Vector3 startLocalPos;
    Quaternion startLocalRot;

    void Start()
    {
        // salva posição/rotação inicial
        startLocalPos = transform.localPosition;
        startLocalRot = transform.localRotation;
    }

    void Update()
    {
        float t = Time.time;

        // movimento suave (senóide)
        float px = Mathf.Sin(t * positionSpeed) * positionAmount;
        float py = Mathf.Cos(t * positionSpeed * 0.8f) * positionAmount;

        // ruído (clássico estilo Amenti)
        float nx = (Mathf.PerlinNoise(t * noiseFrequency, 0f) - 0.5f) * positionAmount;
        float ny = (Mathf.PerlinNoise(0f, t * noiseFrequency) - 0.5f) * positionAmount;

        // aplica posição balançada
        transform.localPosition = startLocalPos
            + new Vector3(px + nx, py + ny, 0f);

        // rotação suave com leve instabilidade
        float rx = Mathf.Sin(t * rotationSpeed) * rotationAmount;
        float ry = Mathf.Cos(t * rotationSpeed * 1.1f) * rotationAmount;

        float nrx = (Mathf.PerlinNoise(t * noiseFrequency, 2f) - 0.5f) * rotationAmount;
        float nry = (Mathf.PerlinNoise(3f, t * noiseFrequency) - 0.5f) * rotationAmount;

        transform.localRotation = Quaternion.Euler(
            startLocalRot.eulerAngles.x + rx + nrx,
            startLocalRot.eulerAngles.y + ry + nry,
            startLocalRot.eulerAngles.z
        );
    }
}
