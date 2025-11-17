using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocarCena : MonoBehaviour
{
    /* ============================================================
       SESSÃO 1 — CONFIGURAÇÕES GERAIS
       Contém variáveis públicas ajustáveis no inspetor.
    ============================================================ */

    [Header("Configuração")]
    public string nomeDaCena;                 // Nome da cena que será carregada
    public float delayAntesDeTrocar = 0f;     // Tempo extra de espera antes de trocar a cena

    /* ============================================================
       SESSÃO 2 — CONFIGURAÇÕES DE FADE
       Objeto responsável pelo efeito de transição.
    ============================================================ */

    [Header("Fade")]
    public GameObject fadeObject;             // Objeto contendo o Animator do fade
    private Animator fadeAnimator;            // Referência ao Animator do fade

    /* ============================================================
       SESSÃO 3 — INICIALIZAÇÃO
       Busca o Animator no objeto de fade caso exista.
    ============================================================ */

    void Start()
    {
        if (fadeObject != null)
            fadeAnimator = fadeObject.GetComponent<Animator>();
    }

    /* ============================================================
       SESSÃO 4 — MÉTODO PÚBLICO PARA TROCAR A CENA
       Pode ser chamado por botões, eventos ou outros scripts.
    ============================================================ */

    public void CarregarCena()
    {
        StartCoroutine(FadeAndLoad());
    }

    /* ============================================================
       SESSÃO 5 — ROTINA DE FADE E CARREGAMENTO
       Executa o fade, espera o delay e carrega a nova cena.
    ============================================================ */

    private System.Collections.IEnumerator FadeAndLoad()
    {
        // Executa o fade se o Animator estiver configurado
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("FadeOut");   // Inicia a animação de fade
            yield return new WaitForSeconds(1f); // Espera pela duração do fade (ajuste conforme necessário)
        }

        // Espera o delay configurado no inspetor
        if (delayAntesDeTrocar > 0)
            yield return new WaitForSeconds(delayAntesDeTrocar);

        // Carrega a cena definida
        SceneManager.LoadScene(nomeDaCena);
    }
}
