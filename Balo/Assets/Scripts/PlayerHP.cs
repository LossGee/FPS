using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 체력을 표현하고 싶다. 

public class PlayerHP : MonoBehaviour
{
    int hpPotion;
    public Text textHpPotion;
    public int HP_POTION
    {
        get { return hpPotion; }
        set
        {
            hpPotion = value;
            textHpPotion.text = "X " + hpPotion;
        }
    }


    int hp;
    public int maxHP = 5;
    public GameObject[] hpObjectList;
    public int HP           //Property
    {
        get { return hp; }
        set
        {
            hp = value;
            //print(hp);
            
            // 체력이 소진되었는지에 대한 활성처리
            for (int i = 0; i < hpObjectList.Length; i++)
            {
                hpObjectList[i].SetActive(!(i<hp));
            }
        }
    }

    public void ResetHpObject()
    {
        // 최대채력 갯수만큼 체력 UI를 활성화 해야한다. 
        for (int i = 0; i < hpObjectList.Length; i++)
        {
            hpObjectList[i].transform.parent.gameObject.SetActive(i < maxHP);
        }
    }

    public void AddHP(int value)
    {
        maxHP += value;
        if (maxHP > hpObjectList.Length)
        {
            maxHP = hpObjectList.Length;
        }
        ResetHpObject();
        //HP = HP;
    }

    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        ResetHpObject();
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 만약 1번키를 누르면
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            // 2. 만약 포션이 1개 이상이라면 
            if (HP_POTION > 0)
            {
                // 3. 포션을 1 감소시키고 
                HP_POTION--;
                print(HP_POTION);

                // 4. 체력을 1 증가시키고 싶다. 
                HP++;
            }
        }
    }
}
