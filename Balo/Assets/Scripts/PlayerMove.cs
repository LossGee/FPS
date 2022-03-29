using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
step1) 사용자의 입력에 따라 앞뒤좌우로 이동하고 싶다.
step2) + 이동방향(dir)을 "카메라"를 기준으로 이동하고 싶다.
step3) Jump를 뛰게 하고 싶다. (필요한것: 뛰는 힘, 중력, y속도)
        이때, 
step4) 다중 점프를 뛰고 싶다. 

*/
public class PlayerMove : MonoBehaviour
{
    CharacterController cc;

    public float speed = 5.0f;      // P = P0 + vt에 활용하는 v값

    public float jumpPower = 10f;
    public float gravity = -9.81f;
    float yVelocity;

    int jumpCount;                  // 다중 점프를 하고싶다
    public int maxJumpCount = 2;           // 다중 점프 최대 횟수 제한
   
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();   //<> 안에 들어간 반환자료형이 실행되었을 때 결정 됨. (using System.Collections.Generic;에 대한 내용)
    }

    // Update is called once per frame
    void Update()
    {
        // [Player Jump 기능 구현]
        // 1. y속도는 중력을 계속 받아야 한다. (중력: -9.81 m/s)
        yVelocity += gravity * Time.deltaTime;      // 1초당 gravity를 한번씩 받아야 하므로 이를 나누어 받게 한다고 생각

        // 만약 땅에 서있다면(= '~가 어디에 맞닿아 있다면'과 같은 방식으로 구현)
        //cc.collisionFlags & CollisionFlags.Below) != 0       // bit mask를 활용한 것
        //cc.isGrounded      // 위 문장이 줄여져 Unity에서 제공하는 cc.isGrounded
        //if(cc.collisionFlags == CollisionFlags.Below)
        //{
        //    yVelocity = 0;
        //}

        if (cc.isGrounded)
        {
            // 만약 땅에 서 있다면 점프 회수를 0으로 초기화
            jumpCount = 0;
            yVelocity = 0;
        }
        else 
        {
            // 만약, 공중이라면
            yVelocity += gravity * Time.deltaTime;
        }
        // yVelocity를 0으로 하고싶다. 
        

        // 2. 만약 땅에 서 있고 점프키를 누르면 y속도를 jumpPower로 대입하고 싶다.
        //    만약, 점프횟수가 최대점프횟수보다 작고 점프 버튼을 누르면 y속도에 jumpPower로 대입하고 싶ㄴ다
        if (jumpCount < maxJumpCount && Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
            jumpCount++;
        }
        // 3. 이동방향의 y속성에 y속도를 대입하고 싶다. 
        // 아래 dir.Normalize(); 수행된 후 넣어주도록 코드 작성


        // [Player 이동 기능 구현]
        // 1. 사용자의 입력에 따라 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. 앞뒤좌우로 방향을 만들고 
        Vector3 dir = new Vector3(h, 0, v);
        //dir = dir.normalized; // .normalized 변수: 크기가 1인 nomalized의 사본을 생성한다. 
        // + "카메라"를 기준으로 방향을 결정하고 싶다.
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;              // 캐릭터가 수직방향으로 붕 떠서 움직이는 것을 방지(y방향으로 바라보고 앞으로갔을 때 생기는 문제)   
        dir.Normalize();        // Normalize() 함수: dir 원본 자체를  1로 만듦 (현재 목적에는 이게 더 부합)

        Vector3 velocity = dir * speed;
        velocity.y = yVelocity;         // speed가 yVelocity에 곱해지는 것을 방지

        // cc를 사용하면서 캐릭터를 특정좌표로 순간이동 시키고 싶다면? 
        //cc.enabled = false;
        //transform.position = new Vector3(0, 0, 0);
        //cc.enabled = true;

        // 3. 그 방향으로 이동하고 싶다. 
        cc.Move(velocity * Time.deltaTime);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 from = transform.position;
        Vector3 to = transform.position + Vector3.up * yVelocity;
        Gizmos.DrawLine(from, to);
    }
}
