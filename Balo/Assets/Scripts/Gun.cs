using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 만약 마우스 왼쪽 버튼을 누르면
// 메인카메라가 바라본곳에 총알 자국을 남기고 싶다. 
// 1. 사용자의 마우스 왼쪽 버튼 입력받기
// 2. Ray를 생성하고 '발사될 위치'와 '진행방향' 설정
// 3. "Ray가 부딪힌 대상의 정보를 저장할 변수" 생성하기 
// 4. "Ray를 발사"하고, 만일 부딪힌 물체가 있으면 피격이펙트 표시하기
public class Gun : MonoBehaviour
{
    private void Awake()
    {
        //Cursor.visible = false;                   // 마우스 커서 끄기 
        Cursor.lockState = CursorLockMode.Locked;   // 게임 실행시 마우스 위치를 화면 중앙으로 고정시킨다. 
        Cursor.lockState = CursorLockMode.Confined; // 마우스 커서가 화면 밖으로 나가지 못하게 하는 기능  

    }

    public GameObject bImpactFactory;           // 총알공장 
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
    Vector3 gunTargetPosition;      // Gun의 목적지를 담을 position   

    private void UpdateZoom()
    {
        // 만약 마우스 오른쪽 버튼을 누르고 있으면 
        if (Input.GetButton("Fire2"))
        {
            // ZoomIn(확대)을 하고 싶다. 
            targetZoomValue = zoomInValue;
            //Camera.main.fieldOfView = zoomInValue;

            // Zoom in 일때, 총 모델의 위치 
            gunTargetPosition = zoomInPos.position;
        }
        // 그렇지 않고 떼면
        else if (Input.GetButtonUp("Fire2"))
        {
            // ZoomOut(원래대로)을 하고 싶다. 
            targetZoomValue = zoomOutValue;

            // Zoom out일때, 총 모델의 위치 
            gunTargetPosition = zoomOutPos.position;

        }
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetZoomValue, Time.deltaTime * 10);

        gunObject.localPosition = Vector3.Lerp(gunObject.position, gunTargetPosition, Time.deltaTime * 10);
    }

    void UpdateFire()
    {
        // 만약 마우스 왼쪽 버튼을 누르면
        if (Input.GetButtonDown("Fire1"))
        {
            // 메인카메라가 바라본곳에 총알 자국을 남기고 싶다.
            // 시선, 바라보다, 닿은 곳의 정보
            // 메인카메라의 위치에서 메인카메라의 앞방향으로 시선을 만들고 싶다. 
            // (아래 Ray~ if까지는 하나의 형식or구문임. 그냥 외우자)
            /*
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            { 
            
            }
            */
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hitInfo;     // 부디진 대상의 정보를 저장할 변수
            if (Physics.Raycast(ray, out hitInfo))
            {
                //print(hitInfo.transform.name);
                // 총알자국을 부빚힌 곳에 남기고싶다.
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
                //bImpact = Instantiate(isEnemy ? bImpactForEnemyFactory : bImpactFactory);     // 3항 연산자로 위 내용 한번에 표한하기 

                bImpact.transform.position = hitInfo.point;
                bImpact.transform.forward = hitInfo.normal;     // 부딪힌 대상의 법선 벡터(normal)

                // 만약 Enemy라면 
                if (isEnemy)
                {
                    Enemy enemy = hitInfo.collider.GetComponent<Enemy>();

                    // 너 총에 맞았어 라고 알려주고 싶다.
                    // 데미지 값를 알려주고 싶다.
                    if (enemy != null)
                    {
                        enemy.TryDamage(damage);
                    }
                }
            }

        }
    }
}
