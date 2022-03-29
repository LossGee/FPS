using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
    step1) agent 기능을 이용해서 목적지를 향해서 이동하고 싶다
    step2) FSM : 목표검색(대기, Idle), 이동, 공격
    
 */
public class Enemy : MonoBehaviour
{
    // "열거형(enumaration)" 으로 state들 정의
    // SEARTCH = 100 이렇게 임의로 줄 수 있으며 그 뒤에         Idle,오는 변수들은 그 다음 숫자인 101이 옴
    enum State
    {
        Idle,
        Move,
        Attack,
        React,
        Death
    }
    // 현재 상태
    State state;

    void SetState(State next, string animationName = null)
    {
        state = next;

        if (null != animationName)
        {
            anim.SetTrigger("Idle");
        }

    }

    NavMeshAgent agent;
    GameObject target;      // GameObject: Call by Reference
    public Animator anim;   // 애니메이터를 가져오기 위한 변수
    EnemyHP enemyHP;
    
    // 총에 맞으면 0.1초동안 MAT_White 재질로 바뀌고 싶다. 
    SkinnedMeshRenderer[] rendList;
    public Material matWhite;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;     // 초기 상태 지정
        agent = this.gameObject.GetComponent<NavMeshAgent>();       //this.gameObject 생략 가능
        enemyHP = GetComponent<EnemyHP>();
        rendList = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // [FSM: (way1)if-else를 활용하여 구현]
        // 만약, 현재상태(state)가 목표검색(Idle)이라면 검색만 하고 싶다. 
        if (state == State.Idle)
        {
            UpdateIdle();
        }
        // 그렇지않고 상태가 이동이라면 이동만 하고싶다. 
        else if (state == State.Move)
        {
            UpdateMove();
        }
        // 그렇지않고 상태가 공격이라면 공격만 하고싶다. 
        else if (state == State.Attack)
        {
            UpdateAttack();
        }
        */

        // [FSM: (way2)switch-case문을 활용하여 구현] - 아래처럼 한 줄로 정리할 수 있다. 
        switch (state)
        {
            // 만약, 현재상태(state)가 목표검색(Idle)이라면 검색만 하고 싶다. 
            case State.Idle: UpdateIdle(); break;
            // 그렇지않고 상태가 이동이라면 이동만 하고싶다
            case State.Move: UpdateMove(); break;
            // 그렇지않고 상태가 공격이라면 공격만 하고싶다.
            case State.Attack: UpdateAttack(); break;

        }
    }
    private void UpdateDeath()
    {

    }

    private void UpdateReactk()
    {

    }

    private void UpdateIdle()
    {
        // 목적지를 찾고 싶다.
        target = GameObject.Find("Player");
        // 만약 목적지를 찾았으면(target이 없으면 NULL이 반환된다는 것을 활용)
        if (target != null)
        {
            // 이동상태로 전이하고 싶다. 
            state = State.Move;
            anim.SetTrigger("Move");
        }
    }
    private void UpdateMove()
    {
        // agent에게 목적지를 알려주고 싶다. 
        agent.destination = target.transform.position;      // .destination은 Vector3(structure이므로 Call by Value, Start에 있을 경우 처음 지점으로만 이동. 따라오기 X)

        // 만약 목적지와의 거리가 공격가능 거리라면?
        // == (목적지와의 거리가 <= 공격가능거리)
        float distance = Vector3.Distance(target.transform.position, transform.position);       // 두 벡터 사이의 거리 
        if (distance <= agent.stoppingDistance)
        {
            // 공격상태로 전이하고 싶다. 
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
    }

    private void UpdateAttack()
    {
        // 만약 목적지와의 거리가 공격가능 거리가 아니라면?
        // 이동상태로 전이하고싶다.
    }
    internal void OnEnemyAttackHit()        // internal: 같은 프로세스에서는 public을 다른 프로세스에서는 private로 사용되는 접근제한자
    {
        //print("OnEnemyAttackHit");
        // 만약 공격가능 거리라면  Hit를 하고 싶다. 
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance <= agent.stoppingDistance)
        {
            //print("Hit!!!!!");
            HitManager.Instance.DoHitPlz();     // 싱글톤으로 만든 HitManager에서 만든 코루틴함수가 포함된 DoHitPlz 함수 호출
            PlayerHP php = target.GetComponent<PlayerHP>();
            php.HP--;
            if (php.HP <= 0)
            { 
                // 게임오버
            }
        
        }
    }

    internal void OnEnemyAttackFinished()
    {
        //print("OnEnemyAttackFinished");
        // 만약 공격가능거리가 아니라면
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance > agent.stoppingDistance)
        {
            // 이동 상태로 전이하고 싶다.
            state = State.Move;
            anim.SetTrigger("Move");
        }
    }

    /// <summary>
    /// Player -> Enemy를 공격함. 
    /// </summary>
    public void TryDamage(int damageValue)
    {
        enemyHP.HP -= damageValue;
        agent.isStopped = true;        // agent.isStopped (true: 멈춤 / false: 이동)
        if (enemyHP.HP <= 0)
        {
            // 죽음...
            state = State.Death;
            //anim.ResetTrigger("React");
            anim.SetTrigger("Death");
        }
        else
        {
            // 리액션
            state = State.React;
            anim.SetTrigger("React");
        }
        // 총에 맞으면 0.1초동안 MAT_White재질로 바뀌고 싶다. 
        for (int i=0 ; i < rendList.Length; i++)
        {
            StartCoroutine("IEWhite", i);
        }

    }

    IEnumerator IEWhite(int index)
    {
        SkinnedMeshRenderer rend = rendList[index];
        Material mat = rend.material;
        rend.material = matWhite;
        yield return new WaitForSeconds(0.1f);
        rend.material = mat;
    }

    internal void OnEnemyReactFinished()        // internal: 접근한정자(public같은 거임) >> 다른 프로그램끼리의 접근은 막고 같은 프로그램 안에서는 접근할 수 있도록 하겠다.
    {
        if (state != State.React)
        {
            return;
        }

        //리액션 애니메이션이 종료되는 순간 이동상태로 전이하고 싶다. 
        state = State.Move;
        anim.SetTrigger("Move");
        agent.isStopped = false;
        GetComponent<Collider>().enabled = false;
    }

    internal void OnEnemyDeathFinished()
    {
        // 죽음 애니메이션이 종료되는 순간 스스로 파괴되고 싶다.
        Destroy(gameObject);
    }
}
