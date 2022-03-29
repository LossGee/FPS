using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 태어날때 이동할 속도를 Rigidbody에게 알려주고 싶다. 
// 이동방향은 내 앞방향으로 하고싶다
public class Grenade : MonoBehaviour
{
    public float speed = 10;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        rb.AddTorque(transform.right * 20, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 우리가 만들 것은 충격에 반응하는 고폭탄(만약 수류탄이라면 터지는 일정 시간을 넣어주면 된다.)
    public float radius = 3;
    public GameObject explosionFactory;
    private void OnCollisionEnter(Collision collision)
    {
        // 반경 3m안에 적을 모두 파괴하고 싶다. 
        Collider[] cols =  Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < cols.Length; i++)
        { 
            Enemy enemy = cols[i].GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TryDamage(3);
            }
        }

        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position = transform.position;
        Destroy(gameObject);
    }
}
