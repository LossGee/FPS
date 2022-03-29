using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ü���� ǥ���ϰ� �ʹ�. 

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
            
            // ü���� �����Ǿ������� ���� Ȱ��ó��
            for (int i = 0; i < hpObjectList.Length; i++)
            {
                hpObjectList[i].SetActive(!(i<hp));
            }
        }
    }

    public void ResetHpObject()
    {
        // �ִ�ä�� ������ŭ ü�� UI�� Ȱ��ȭ �ؾ��Ѵ�. 
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
        // 1. ���� 1��Ű�� ������
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            // 2. ���� ������ 1�� �̻��̶�� 
            if (HP_POTION > 0)
            {
                // 3. ������ 1 ���ҽ�Ű�� 
                HP_POTION--;
                print(HP_POTION);

                // 4. ü���� 1 ������Ű�� �ʹ�. 
                HP++;
            }
        }
    }
}
