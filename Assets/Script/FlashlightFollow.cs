using UnityEngine;

[RequireComponent(typeof(Transform))]
public class FlashlightFollow : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;        // Transform do jogador (a lanterna segue este transform)
    public Camera cam;             // Câmera usada para raycast (se vazio, usa Camera.main)

    [Header("Posição / Offset")]
    public Vector3 offsetFromPlayer = new Vector3(0f, 0.5f, 0.6f); // posição da lanterna relativa ao jogador

    [Header("Suavização")]
    [Range(0f, 20f)]
    public float positionSmooth = 12f; // suaviza o movimento da posição
    [Range(0f, 20f)]
    public float rotationSmooth = 12f; // suaviza a rotação (look at)

    [Header("Plano do Raycast")]
    public float planeHeightOffset = 0f; // altura relativa ao jogador onde o plano é criado (ex: 0 = nível do jogador)

    void Reset()
    {
        cam = Camera.main;
    }

    void Start()
    {
        if (cam == null) cam = Camera.main;
        if (player == null)
            Debug.LogWarning("FlashlightFollow: Player não atribuído. Arraste o Transform do jogador no inspector.");
    }

    void LateUpdate()
    {
        if (player == null || cam == null) return;

        // 1) Mover a lanterna para perto do jogador (com offset e suavização)
        Vector3 targetPos = player.position + player.TransformDirection(offsetFromPlayer);
        transform.position = Vector3.Lerp(transform.position, targetPos, 1f - Mathf.Exp(-positionSmooth * Time.deltaTime));

        // 2) Encontrar ponto no mundo correspondente ao mouse (usando um plano horizontal na altura do jogador + offset)
        Plane plane = new Plane(Vector3.up, player.position + Vector3.up * planeHeightOffset);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        float enter;
        Vector3 worldPoint;
        if (plane.Raycast(ray, out enter))
        {
            worldPoint = ray.GetPoint(enter);
        }
        else
        {
            // fallback: se não intersectar (raríssimo), projeta a uma distância fixa
            worldPoint = ray.GetPoint(10f);
        }

        // 3) Fazer a lanterna olhar para o ponto calculado (suavizando a rotação)
        Quaternion targetRot = Quaternion.LookRotation(worldPoint - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 1f - Mathf.Exp(-rotationSmooth * Time.deltaTime));
    }
}
