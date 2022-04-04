using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    void Start()
    {
        //리스트
        List<int> list = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            list.Add(1);
        }
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = i;
        }
        list.RemoveAt(2);       // .RemoveAt(2) 2번 자리에 있는 항목을 제거해주세요 
        list.Remove(2);         // .Revmoe(2) 2라는 데이터를 찾아서 삭제해주세요 
        for (int i = 0; i < list.Count; i++)
        {
            print(list[i]);
        }


        // 배열
        /*int[] a = new int[5];       // 배열 선언
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
