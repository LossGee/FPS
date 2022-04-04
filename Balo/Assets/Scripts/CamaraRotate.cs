using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자가 마우스를 움직이면 카메라를 회전하고 싶다.
// Y축의 회전값을 미니맵 아이콘에도 적용하고 싶다. 
public class CamaraRotate : MonoBehaviour
{
    float rx;
    float ry;
    public float rotSpeed = 300f;   // 마우스 변화량이 매우 작기 때문에 증폭을 위한 변수
    public Transform minimap;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 사용자가 마우스를 움직이면 카메라를 회전하고 싶다.
        // 1. 사용자가 마우스 움직임을 입력으로 받는다. 
        float mx = Input.GetAxis("Mouse X");   // Mouse X: 마우스 좌우 움직임의 변화량
        float my = Input.GetAxis("Mouse Y");   // Mouse Y: 마우스 상하 움직임의 변화량

        //print("mouse_X=" + mx + ", mouse_Y=" + my);

        //rotate_X = mouse_Y;     // mouse_X와 mouse_Y는 변화량이기 때문에 "누적"을 해주어야 한다
        //rotate_Y = mouse_X;
        rx += my * rotSpeed * Time.deltaTime;       // 모두가 공평하게 하기 위해서 Time.deltaTime 곱해주기(but 잘 안움직이게 됨.) >> 해결 - 200배 증폭시키기
        ry += mx * rotSpeed * Time.deltaTime;

        rx = Mathf.Clamp(rx, -70f, 70f);        //Clamp(제한대상, min, max) - 제한 대상이 min보다 작으면 min으로 max보다 크면 max로 대체된다. 

        // 2. 카메라를 회전하고 싶다. (Quarternion(사원수)를 사용하기도 함)
        //transform.Rotate(new Vector3(rotate_X, rotate_Y, 0));         // 이렇게 실행했을 때 원하는 대로 움직이지 않음.
        transform.eulerAngles = new Vector3(-rx, ry, 0);

        //Quaternion q = Quaternion.Euler(0, 10, 0);
        //transform.rotation *= transform.rotation * q;

        minimap.eulerAngles = new Vector3(90, ry, 0);
    }
}
