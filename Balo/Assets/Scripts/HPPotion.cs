using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPotion : MonoBehaviour
{
    //float currentTime;
    //float restoreTime = 1;

    //private void OnTriggerStay(Collider other)
    //{
    //    currentTime += Time.fixedDeltaTime;
    //    if (currentTime > restoreTime)
    //    {
    //        PlayerHP playerHP = other.gameObject.GetComponent<PlayerHP>();
    //        // ���� other�� �÷��̾��� 
    //        if (other.tag == "Player")
    //        {
    //            if (playerHP != null)
    //            {
    //                // �÷��̾��� ü��(PlayerHP������Ʈ)�� 1 ������Ű�� �ʹ�.
    //                playerHP.HP++;
    //                Destroy(gameObject);
    //            }
    //        }
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{

    //    PlayerHP playerHP = other.gameObject.GetComponent<PlayerHP>();
    //    // ���� other�� �÷��̾��� 
    //    if (other.tag == "Player")
    //    {
    //        if (playerHP != null)
    //        {
    //            // �÷��̾��� ü��(PlayerHP������Ʈ)�� 1 ������Ű�� �ʹ�.
    //            playerHP.HP++;
    //            Destroy(gameObject);
    //        }
    //    }
    //}
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}


    //������ ȹ��
    private void OnTriggerEnter(Collider other)
    {
        PlayerHP playerHP = other.gameObject.GetComponent<PlayerHP>();
        // ���� other�� �÷��̾��� 
        if (other.tag == "Player")
        {
            if (playerHP != null)
            {
                // �÷��̾��� ü��(PlayerHP������Ʈ)�� 1 ������Ű�� �ʹ�.
                playerHP.HP_POTION++;
                Destroy(gameObject);
            }
        }
    }
}
