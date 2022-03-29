using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPAddPotion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerHP playerHP = other.gameObject.GetComponent<PlayerHP>();
        // 만약 other가 플레이어라면 
        if (other.tag == "Player")
        {
            if (playerHP != null)
            {
                // 플레이어의 최대 체력(PlayerHP컴포넌트)을 1 영구증가시키고 싶다.
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
