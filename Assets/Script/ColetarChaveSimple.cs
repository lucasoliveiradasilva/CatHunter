using UnityEngine;
using UnityEngine.UI;

public class ColetarChaveSimples : MonoBehaviour
{
    [Header("Item e som")]
    public string itemName = "Chave";         // nome do item
    public Sprite itemIcon;                    // ícone do item para hotbar
    public AudioClip pickupSound;              // som da coleta
    public float detectDistance = 3f;         // alcance para mostrar "Chave (E)"

    [Header("Referências")]
    public Image[] hotbarSlots;                // slots da hotbar
    public Text hintText;                      // texto de dica "Chave (E)"

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hintText.enabled = false;
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        // verifica se o player está olhando para o objeto
        RaycastHit hit;
        bool mirando = Physics.Raycast(
            Camera.main.transform.position,
            Camera.main.transform.forward,
            out hit,
            detectDistance
        ) && hit.collider.gameObject == this.gameObject;

        if (mirando)
        {
            // mostra dica
            hintText.text = itemName + " (E)";
            hintText.enabled = true;

            // coleta
            if (Input.GetKeyDown(KeyCode.E))
            {
                // toca som
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

                // adiciona ícone na hotbar
                for (int i = 0; i < hotbarSlots.Length; i++)
                {
                    if (hotbarSlots[i].sprite == null)
                    {
                        hotbarSlots[i].sprite = itemIcon;
                        hotbarSlots[i].color = Color.white;
                        break;
                    }
                }

                // remove objeto do mundo
                Destroy(gameObject);

                // desativa dica
                hintText.enabled = false;
            }
        }
        else
        {
            if (hintText.enabled)
                hintText.enabled = false;
        }
    }
}
