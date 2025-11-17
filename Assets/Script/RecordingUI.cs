using UnityEngine;
using UnityEngine.UI;
using TMPro; // Necessário para TextMeshPro

public class RecordingUI : MonoBehaviour
{
    [Header("Referências UI")]
    public Image blinkingImage;   // Image que vai piscar
    public TMP_Text timerText;    // Texto do contador (TextMeshPro)

    [Header("Configurações Piscar")]
    public float blinkSpeed = 1f; // Velocidade do piscar

    private float elapsedTime = 0f; // Tempo decorrido em segundos
    private bool isRecording = true; // Começa gravando automaticamente

    void Start()
    {
        // Inicializa a opacidade da imagem como transparente
        if (blinkingImage != null)
        {
            Color c = blinkingImage.color;
            c.a = 0f;
            blinkingImage.color = c;
        }

        // Inicializa o timer mostrando 00:00
        if (timerText != null)
        {
            timerText.text = "00:00";
        }
    }

    void Update()
    {
        if (isRecording)
        {
            // Atualiza o tempo decorrido
            elapsedTime += Time.deltaTime;

            // Converte para minutos e segundos
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);

            // Atualiza o texto TMP com formato mm:ss
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            // Faz a imagem piscar alterando a opacidade
            if (blinkingImage != null)
            {
                float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1f);
                Color c = blinkingImage.color;
                c.a = alpha;
                blinkingImage.color = c;
            }
        }
    }

    // Método público caso queira parar a gravação manualmente
    public void StopRecording()
    {
        isRecording = false;

        // Deixa a imagem transparente quando parar
        if (blinkingImage != null)
        {
            Color c = blinkingImage.color;
            c.a = 0f;
            blinkingImage.color = c;
        }
    }
}
