using UnityEngine;
using UnityEngine.EventSystems;

public class EfeitoBotao : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /* ============================================================
       SESSÃO 1 — CONFIGURAÇÕES DO EFEITO
       Define o quanto o botão cresce e a velocidade da animação.
    ============================================================ */

    public float tamanhoMaximo = 1.1f;   // Fator máximo de aumento do botão
    public float velocidade = 5f;        // Velocidade do efeito de pulsação

    /* ============================================================
       SESSÃO 2 — VARIÁVEIS INTERNAS
       Armazena estado e valores originais do botão.
    ============================================================ */

    private Vector3 tamanhoOriginal;     // Tamanho inicial do botão
    private bool mouseDentro = false;    // Indica se o cursor está sobre o botão

    /* ============================================================
       SESSÃO 3 — INICIALIZAÇÃO
       Captura o tamanho original do botão ao iniciar.
    ============================================================ */

    void Start()
    {
        tamanhoOriginal = transform.localScale;
    }

    /* ============================================================
       SESSÃO 4 — ATUALIZAÇÃO DO EFEITO
       Executado a cada frame; controla pulsação e retorno.
    ============================================================ */

    void Update()
    {
        if (mouseDentro)
        {
            // Calcula um valor oscilante usando seno para criar pulsação
            float t = (Mathf.Sin(Time.time * velocidade) + 1f) / 2f;

            // Faz o botão pulsar entre o tamanho original e o tamanho ampliado
            transform.localScale = Vector3.Lerp(
                tamanhoOriginal,
                tamanhoOriginal * tamanhoMaximo,
                t
            );
        }
        else
        {
            // Retorna suavemente ao tamanho original quando o mouse sai
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                tamanhoOriginal,
                Time.deltaTime * velocidade
            );
        }
    }

    /* ============================================================
       SESSÃO 5 — DETECÇÃO DO CURSOR
       Métodos disparados quando o mouse entra ou sai da área do botão.
    ============================================================ */

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseDentro = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseDentro = false;
    }
}
