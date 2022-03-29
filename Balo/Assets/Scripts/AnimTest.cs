using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();      // 자식 쪽으로 타고 내려가며 Component 검색
    
    }

    // Update is called once per frame
    void Update()
    {
        // 눌리는 버튼에 따라 Animation 제어할 수 있도록 지정
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Idle 동작
            anim.SetTrigger("Idle");        // Animated blemd 과정이 들어감
            //anim.CrossFade();         // CrossFade()를 사용할 수도 있음. 
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            anim.Play("Idle", 0, 0);        // Animate blend 과정 없이 바로 해당 애니메이션으로 넘어감
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Walk 동작
            anim.SetTrigger("Walk");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Attack 동작
            anim.SetTrigger("Attack");
        }
    }
}
