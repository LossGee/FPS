using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���콺 ���� ��ư�� ������
// ����ī�޶� �ٶ󺻰��� �Ѿ� �ڱ��� ����� �ʹ�. 
// 1. ������� ���콺 ���� ��ư �Է¹ޱ�
// 2. Ray�� �����ϰ� '�߻�� ��ġ'�� '�������' ����
// 3. "Ray�� �ε��� ����� ������ ������ ����" �����ϱ� 
// 4. "Ray�� �߻�"�ϰ�, ���� �ε��� ��ü�� ������ �ǰ�����Ʈ ǥ���ϱ�
public class Gun : MonoBehaviour
{
    private void Awake()
    {
        //Cursor.visible = false;                   // ���콺 Ŀ�� ���� 
        Cursor.lockState = CursorLockMode.Locked;   // ���� ����� ���콺 ��ġ�� ȭ�� �߾����� ������Ų��. 
        Cursor.lockState = CursorLockMode.Confined; // ���콺 Ŀ���� ȭ�� ������ ������ ���ϰ� �ϴ� ���  

    }

    public GameObject bImpactFactory;           // �Ѿ˰��� 
    public GameObject bImpactForEnemyFactory;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        //gunTargetPosition = zoomInPos.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateZoom();
        UpdateFire();
    }

    public float zoomInValue = 15;
    public float zoomOutValue = 60;
    float targetZoomValue = 60;
    public Transform zoomInPos;
    public Transform zoomOutPos;
    public Transform gunObject;
    Vector3 gunTargetPosition;      // Gun�� �������� ���� position   

    private void UpdateZoom()
    {
        // ���� ���콺 ������ ��ư�� ������ ������ 
        if (Input.GetButton("Fire2"))
        {
            // ZoomIn(Ȯ��)�� �ϰ� �ʹ�. 
            targetZoomValue = zoomInValue;
            //Camera.main.fieldOfView = zoomInValue;

            // Zoom in �϶�, �� ���� ��ġ 
            gunTargetPosition = zoomInPos.position;
        }
        // �׷��� �ʰ� ����
        else if (Input.GetButtonUp("Fire2"))
        {
            // ZoomOut(�������)�� �ϰ� �ʹ�. 
            targetZoomValue = zoomOutValue;

            // Zoom out�϶�, �� ���� ��ġ 
            gunTargetPosition = zoomOutPos.position;

        }
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetZoomValue, Time.deltaTime * 10);

        gunObject.localPosition = Vector3.Lerp(gunObject.position, gunTargetPosition, Time.deltaTime * 10);
    }

    void UpdateFire()
    {
        // ���� ���콺 ���� ��ư�� ������
        if (Input.GetButtonDown("Fire1"))
        {
            // ����ī�޶� �ٶ󺻰��� �Ѿ� �ڱ��� ����� �ʹ�.
            // �ü�, �ٶ󺸴�, ���� ���� ����
            // ����ī�޶��� ��ġ���� ����ī�޶��� �չ������� �ü��� ����� �ʹ�. 
            // (�Ʒ� Ray~ if������ �ϳ��� ����or������. �׳� �ܿ���)
            /*
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            { 
            
            }
            */
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hitInfo;     // �ε��� ����� ������ ������ ����
            if (Physics.Raycast(ray, out hitInfo))
            {
                //print(hitInfo.transform.name);
                // �Ѿ��ڱ��� �κ��� ���� �����ʹ�.
                GameObject bImpact;
                bool isEnemy = hitInfo.collider.CompareTag("Enemy");
                if (isEnemy)
                {
                    bImpact = Instantiate(bImpactForEnemyFactory);
                }
                else
                {
                    bImpact = Instantiate(bImpactFactory);
                }
                //bImpact = Instantiate(isEnemy ? bImpactForEnemyFactory : bImpactFactory);     // 3�� �����ڷ� �� ���� �ѹ��� ǥ���ϱ� 

                bImpact.transform.position = hitInfo.point;
                bImpact.transform.forward = hitInfo.normal;     // �ε��� ����� ���� ����(normal)

                // ���� Enemy��� 
                if (isEnemy)
                {
                    Enemy enemy = hitInfo.collider.GetComponent<Enemy>();

                    // �� �ѿ� �¾Ҿ� ��� �˷��ְ� �ʹ�.
                    // ������ ���� �˷��ְ� �ʹ�.
                    if (enemy != null)
                    {
                        enemy.TryDamage(damage);
                    }
                }
            }

        }
    }
}
