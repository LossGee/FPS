using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ڰ� ���콺�� �����̸� ī�޶� ȸ���ϰ� �ʹ�.
// Y���� ȸ������ �̴ϸ� �����ܿ��� �����ϰ� �ʹ�. 
public class CamaraRotate : MonoBehaviour
{
    float rx;
    float ry;
    public float rotSpeed = 300f;   // ���콺 ��ȭ���� �ſ� �۱� ������ ������ ���� ����
    public Transform minimap;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ����ڰ� ���콺�� �����̸� ī�޶� ȸ���ϰ� �ʹ�.
        // 1. ����ڰ� ���콺 �������� �Է����� �޴´�. 
        float mx = Input.GetAxis("Mouse X");   // Mouse X: ���콺 �¿� �������� ��ȭ��
        float my = Input.GetAxis("Mouse Y");   // Mouse Y: ���콺 ���� �������� ��ȭ��

        //print("mouse_X=" + mx + ", mouse_Y=" + my);

        //rotate_X = mouse_Y;     // mouse_X�� mouse_Y�� ��ȭ���̱� ������ "����"�� ���־�� �Ѵ�
        //rotate_Y = mouse_X;
        rx += my * rotSpeed * Time.deltaTime;       // ��ΰ� �����ϰ� �ϱ� ���ؼ� Time.deltaTime �����ֱ�(but �� �ȿ����̰� ��.) >> �ذ� - 200�� ������Ű��
        ry += mx * rotSpeed * Time.deltaTime;

        rx = Mathf.Clamp(rx, -70f, 70f);        //Clamp(���Ѵ��, min, max) - ���� ����� min���� ������ min���� max���� ũ�� max�� ��ü�ȴ�. 

        // 2. ī�޶� ȸ���ϰ� �ʹ�. (Quarternion(�����)�� ����ϱ⵵ ��)
        //transform.Rotate(new Vector3(rotate_X, rotate_Y, 0));         // �̷��� �������� �� ���ϴ� ��� �������� ����.
        transform.eulerAngles = new Vector3(-rx, ry, 0);

        //Quaternion q = Quaternion.Euler(0, 10, 0);
        //transform.rotation *= transform.rotation * q;

        minimap.eulerAngles = new Vector3(90, ry, 0);
    }
}
