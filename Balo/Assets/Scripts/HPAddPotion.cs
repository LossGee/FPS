using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPAddPotion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerHP playerHP = other.gameObject.GetComponent<PlayerHP>();
        // ���� other�� �÷��̾��� 
        if (other.tag == "Player")
        {
            if (playerHP != null)
            {
                // �÷��̾��� �ִ� ü��(PlayerHP������Ʈ)�� 1 ����������Ű�� �ʹ�.
                //playerHP.maxHP++;
                playerHP.AddHP(1);
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
