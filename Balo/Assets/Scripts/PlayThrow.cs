using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 사용자가 G키를 누르면 폭탄공장에 폭탄을 만들어서 내 위치에 배치하고싶다. 
// step1)폭탄의 앞방향을 내가 던지고자하는 방향(내 앞으로 45도)으로 회전시키고 싶다. 
// step2)폭탄의 앞방향을 내가 던지고자하는 방향(내 앞으로 50도)으로 회전시키고 싶다. 
public class PlayerThrow : MonoBehaviour
{
    public GameObject grenadeFactory;
    public Transform grenadeThrowPosition;
    public float throwPower = 10f;

    // Update is called once per frame
    void Update()
    {
        // 1. 만약 사용자가 G키를 누르면 
        if (Input.GetKeyDown(KeyCode.G))
        {
            // 2. 폭탄공장에서 폭탄을 만들어서 
            GameObject grenade = Instantiate(grenadeFactory);
            // 3. 내위치에 배치하고 싶다. 
            grenade.transform.position = grenadeThrowPosition.position;
            // 4. 폭탄의 앞방향을 내가 던지고자 하는 방향(내앞으로 45도(으로 회전시키고 싶다.)
            // way1) 45도로 던지기
            //Vector3 dir = transform.forward + transform.up;
            //dir.Normalize();
            //grenade.transform.position += dir * throwPower;

            // way2) 내가 원하는 각도로 던지기
            Quaternion q = grenadeThrowPosition.transform.rotation * Quaternion.Euler(-50, 0, 0);   //Quaternion은 *연산을 하면 각도를 더하는 것처럼 만들어짐
            grenade.transform.rotation = q;

            // way3) 총구방향으로 던지기
            //grenade.transform.forward = grenadeThrowPosition.forward;

        }

    }
}
