using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    void Start()
    {
        //����Ʈ
        List<int> list = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            list.Add(1);
        }
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = i;
        }
        list.RemoveAt(2);       // .RemoveAt(2) 2�� �ڸ��� �ִ� �׸��� �������ּ��� 
        list.Remove(2);         // .Revmoe(2) 2��� �����͸� ã�Ƽ� �������ּ��� 
        for (int i = 0; i < list.Count; i++)
        {
            print(list[i]);
        }


        // �迭
        /*int[] a = new int[5];       // �迭 ����
        a[0] = 1;
        for (int i = 0; i < 5; i++)
        {
            a[i] = i;
        }*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}
