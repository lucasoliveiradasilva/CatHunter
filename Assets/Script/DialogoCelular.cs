using System.Collections;
using UnityEngine;
using TMPro;

/* ============================================================
   DIALOGO CELULAR — SIMPLIFICADO COM INTERVALO E AÇÃO FINAL
   • Delay antes da primeira fala e entre todas as falas
   • Espaçamento configurável entre falas
   • Ativa um GameObject ao final do diálogo
============================================================ */

public class DialogoCelular : MonoBehaviour
{
    [Header("REFERÊNCIAS")]
    public TMP_Text chatText;                  // TMP principal para mensagens
    public GameObject acaoFinal;               // GameObject que será ativado no final

    [Header("CONFIGURAÇÕES")]
    public float intervaloEntreFalas = 2f;     // Delay antes da primeira fala e entre as falas
    public int linhasEntreFalas = 1;           // Linhas em branco entre falas

    [System.Serializable]
    public class Mensagem
    {
        public bool esquerda;                  // Lado da fala
        [TextArea]
        public string texto;                   // Texto da mensagem
    }

    [Header("MENSAGENS")]
    public Mensagem[] mensagens;

    void Start()
    {
        chatText.text = "";
        if (acaoFinal != null)
            acaoFinal.SetActive(false);       // garante que está desativado no início

        StartCoroutine(RodarDialogo());
    }

    /* ============================================================
       SESSÃO 1 — RODAR DIALOGO COM INTERVALO CONFIGURÁVEL
    ============================================================ */
    IEnumerator RodarDialogo()
    {
        for (int i = 0; i < mensagens.Length; i++)
        {
            // Espera antes de mostrar cada mensagem
            yield return new WaitForSeconds(intervaloEntreFalas);

            AdicionarMensagem(mensagens[i]);

            // Opcional: pequena pausa de frame para atualizar TMP
            yield return null;

            // Se for a última fala, ativa o GameObject
            if (i == mensagens.Length - 1 && acaoFinal != null)
            {
                acaoFinal.SetActive(true);
            }
        }
    }

    /* ============================================================
       SESSÃO 2 — ADICIONAR MENSAGEM COM ESPAÇAMENTO
    ============================================================ */
    void AdicionarMensagem(Mensagem m)
    {
        string alinh = m.esquerda ? "<align=left>" : "<align=right>";
        string espaco = new string('\n', linhasEntreFalas); // cria linhas em branco

        chatText.text += $"{espaco}{alinh}{m.texto}</align>";
    }
}
