using UnityEngine;

public class BillboardHorizontal : MonoBehaviour
{
    [Header("Referência da câmera")]
    public Transform targetCamera; // A câmera que o objeto vai olhar

    void Start()
    {
        // Se não tiver câmera definida, pega a Main Camera automaticamente
        if (targetCamera == null && Camera.main != null)
        {
            targetCamera = Camera.main.transform;
        }
    }

    void Update()
    {
        if (targetCamera == null) return;

        // Pega a posição da câmera
        Vector3 lookDirection = targetCamera.position - transform.position;

        // Zera o eixo Y para que a rotação seja apenas horizontal
        lookDirection.y = 0;

        if (lookDirection != Vector3.zero)
        {
            // Rotaciona o objeto para olhar para a câmera horizontalmente
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}
