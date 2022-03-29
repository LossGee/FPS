using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
    step1) agent ����� �̿��ؼ� �������� ���ؼ� �̵��ϰ� �ʹ�
    step2) FSM : ��ǥ�˻�(���, Idle), �̵�, ����
    
 */
public class Enemy : MonoBehaviour
{
    // "������(enumaration)" ���� state�� ����
    // SEARTCH = 100 �̷��� ���Ƿ� �� �� ������ �� �ڿ�         Idle,���� �������� �� ���� ������ 101�� ��
    enum State
    {
        Idle,
        Move,
        Attack,
        React,
        Death
    }
    // ���� ����
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
    public Animator anim;   // �ִϸ����͸� �������� ���� ����
    EnemyHP enemyHP;
    
    // �ѿ� ������ 0.1�ʵ��� MAT_White ������ �ٲ�� �ʹ�. 
    SkinnedMeshRenderer[] rendList;
    public Material matWhite;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;     // �ʱ� ���� ����
        agent = this.gameObject.GetComponent<NavMeshAgent>();       //this.gameObject ���� ����
        enemyHP = GetComponent<EnemyHP>();
        rendList = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // [FSM: (way1)if-else�� Ȱ���Ͽ� ����]
        // ����, �������(state)�� ��ǥ�˻�(Idle)�̶�� �˻��� �ϰ� �ʹ�. 
        if (state == State.Idle)
        {
            UpdateIdle();
        }
        // �׷����ʰ� ���°� �̵��̶�� �̵��� �ϰ��ʹ�. 
        else if (state == State.Move)
        {
            UpdateMove();
        }
        // �׷����ʰ� ���°� �����̶�� ���ݸ� �ϰ��ʹ�. 
        else if (state == State.Attack)
        {
            UpdateAttack();
        }
        */

        // [FSM: (way2)switch-case���� Ȱ���Ͽ� ����] - �Ʒ�ó�� �� �ٷ� ������ �� �ִ�. 
        switch (state)
        {
            // ����, �������(state)�� ��ǥ�˻�(Idle)�̶�� �˻��� �ϰ� �ʹ�. 
            case State.Idle: UpdateIdle(); break;
            // �׷����ʰ� ���°� �̵��̶�� �̵��� �ϰ��ʹ�
            case State.Move: UpdateMove(); break;
            // �׷����ʰ� ���°� �����̶�� ���ݸ� �ϰ��ʹ�.
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
        // �������� ã�� �ʹ�.
        target = GameObject.Find("Player");
        // ���� �������� ã������(target�� ������ NULL�� ��ȯ�ȴٴ� ���� Ȱ��)
        if (target != null)
        {
            // �̵����·� �����ϰ� �ʹ�. 
            state = State.Move;
            anim.SetTrigger("Move");
        }
    }
    private void UpdateMove()
    {
        // agent���� �������� �˷��ְ� �ʹ�. 
        agent.destination = target.transform.position;      // .destination�� Vector3(structure�̹Ƿ� Call by Value, Start�� ���� ��� ó�� �������θ� �̵�. ������� X)

        // ���� ���������� �Ÿ��� ���ݰ��� �Ÿ����?
        // == (���������� �Ÿ��� <= ���ݰ��ɰŸ�)
        float distance = Vector3.Distance(target.transform.position, transform.position);       // �� ���� ������ �Ÿ� 
        if (distance <= agent.stoppingDistance)
        {
            // ���ݻ��·� �����ϰ� �ʹ�. 
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
    }

    private void UpdateAttack()
    {
        // ���� ���������� �Ÿ��� ���ݰ��� �Ÿ��� �ƴ϶��?
        // �̵����·� �����ϰ��ʹ�.
    }
    internal void OnEnemyAttackHit()        // internal: ���� ���μ��������� public�� �ٸ� ���μ��������� private�� ���Ǵ� ����������
    {
        //print("OnEnemyAttackHit");
        // ���� ���ݰ��� �Ÿ����  Hit�� �ϰ� �ʹ�. 
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance <= agent.stoppingDistance)
        {
            //print("Hit!!!!!");
            HitManager.Instance.DoHitPlz();     // �̱������� ���� HitManager���� ���� �ڷ�ƾ�Լ��� ���Ե� DoHitPlz �Լ� ȣ��
            PlayerHP php = target.GetComponent<PlayerHP>();
            php.HP--;
            if (php.HP <= 0)
            { 
                // ���ӿ���
            }
        
        }
    }

    internal void OnEnemyAttackFinished()
    {
        //print("OnEnemyAttackFinished");
        // ���� ���ݰ��ɰŸ��� �ƴ϶��
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance > agent.stoppingDistance)
        {
            // �̵� ���·� �����ϰ� �ʹ�.
            state = State.Move;
            anim.SetTrigger("Move");
        }
    }

    /// <summary>
    /// Player -> Enemy�� ������. 
    /// </summary>
    public void TryDamage(int damageValue)
    {
        enemyHP.HP -= damageValue;
        agent.isStopped = true;        // agent.isStopped (true: ���� / false: �̵�)
        if (enemyHP.HP <= 0)
        {
            // ����...
            state = State.Death;
            //anim.ResetTrigger("React");
            anim.SetTrigger("Death");
        }
        else
        {
            // ���׼�
            state = State.React;
            anim.SetTrigger("React");
        }
        // �ѿ� ������ 0.1�ʵ��� MAT_White������ �ٲ�� �ʹ�. 
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

    internal void OnEnemyReactFinished()        // internal: ����������(public���� ����) >> �ٸ� ���α׷������� ������ ���� ���� ���α׷� �ȿ����� ������ �� �ֵ��� �ϰڴ�.
    {
        if (state != State.React)
        {
            return;
        }

        //���׼� �ִϸ��̼��� ����Ǵ� ���� �̵����·� �����ϰ� �ʹ�. 
        state = State.Move;
        anim.SetTrigger("Move");
        agent.isStopped = false;
        GetComponent<Collider>().enabled = false;
    }

    internal void OnEnemyDeathFinished()
    {
        // ���� �ִϸ��̼��� ����Ǵ� ���� ������ �ı��ǰ� �ʹ�.
        Destroy(gameObject);
    }
}