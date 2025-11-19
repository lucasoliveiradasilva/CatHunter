using UnityEngine;
using UnityEngine.UIElements;

public class itembar : MonoBehaviour
{
    public bool slot1;
    public bool slot2;
    public bool slot3;




    private void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("chave1") && slot1 == false)
        {
            slot1 = true;
            gameObject.SetActive(false);
        }


    }

}
