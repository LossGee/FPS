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
    //        // 만약 other가 플레이어라면 
    //        if (other.tag == "Player")
    //        {
    //            if (playerHP != null)
    //            {
    //                // 플레이어의 체력(PlayerHP컴포넌트)을 1 증가시키고 싶다.
    //                playerHP.HP++;
    //                Destroy(gameObject);
    //            }
    //        }
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{

    //    PlayerHP playerHP = other.gameObject.GetComponent<PlayerHP>();
    //    // 만약 other가 플레이어라면 
    //    if (other.tag == "Player")
    //    {
    //        if (playerHP != null)
    //        {
    //            // 플레이어의 체력(PlayerHP컴포넌트)을 1 증가시키고 싶다.
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


    //아이템 획등
    private void OnTriggerEnter(Collider other)
    {
        PlayerHP playerHP = other.gameObject.GetComponent<PlayerHP>();
        // 만약 other가 플레이어라면 
        if (other.tag == "Player")
        {
            if (playerHP != null)
            {
                // 플레이어의 체력(PlayerHP컴포넌트)을 1 증가시키고 싶다.
                playerHP.HP_POTION++;
                Destroy(gameObject);
            }
        }
    }
}
