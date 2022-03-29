using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 화면을 번쩍거리는 기능을 만들고 싶다. 
// ImageHit 게임 오브젝트를 보였다가 0.1초 후에 안보이게 하는 기능을 만들고 싶다. 
public class HitManager : MonoBehaviour
{
    // 싱글톤으로 만드세요!!
    public static HitManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject imageHit;

    void Start()
    {
        imageHit.SetActive(false);
    }

    // ImageHit 게임 오브젝트를 보였다가 0.1초 후에 안보이게 하는 기능을 만들고 싶다.
    public void DoHitPlz()
    {
        StopCoroutine("IEDoHit");
        StartCoroutine("IEDoHit");      // StartCoroutine("코루틴함수") - Coroutine함수를 호출하는 문법!(일반 함수 호출과 다름 주의!!!)
    }

    IEnumerator IEDoHit()     //코루틴 함수의 반환형은 무조건 IEnumerator임
    {
        //print("IEDoHit Coroutine");
        // 보이게 한다.
        imageHit.SetActive(true);
        yield return new WaitForSeconds(0.2f);    //yield 양보, 잠깐 쉴게라는 의미 
        //안보이게 한다. 
        imageHit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
