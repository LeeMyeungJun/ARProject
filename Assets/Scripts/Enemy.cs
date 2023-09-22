using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType {walkenemy,runenemy,bossenemy}  // ���ʹ� Ÿ�� ���� 
enum EnemyState {move,attack} // ���ʹ� ���� ���� (move �� ���ʹ� Ÿ�� ������ ���, attack�� ���ݿ� ���)
public class Enemy : MonoBehaviour
{
    EnemyState enemyState = EnemyState.move;  
    GameObject target;
    NavMeshAgent agent;
    private float _health;
    private float enemyDamage;
    private bool attackAble = true;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        SetStatus(EnemyType.runenemy, GameObject.FindWithTag("Castle"));
        agent.destination = target.transform.position;   // ���ʹ��� Ÿ�� ������ ����.
    }
    void Update()
    {
        if (enemyState == EnemyState.attack && attackAble == true)  // ���ʹ� ���°� ����, attackAble�� Ʈ�� �϶� ���� ����  => �ڷ�ƾ ����
        {
            StartCoroutine("AttackCastle"); 
        }
    }
    public void SetStatus(EnemyType _type, GameObject _target)  // �ش� �Լ��� �������� �� ���ʹ� ����
    {
        target = _target;
       
            switch (_type)
            {
                case EnemyType.walkenemy:
                    agent.speed = 1f;
                    _health = 100f;
                    enemyDamage = 10;
                    break;
                case EnemyType.runenemy:
                    agent.speed = 2f;
                    _health = 200f;
                    enemyDamage = 20;
                    break;
                case EnemyType.bossenemy:
                    agent.speed = 2f;
                    _health = 300f;
                    enemyDamage = 50;
                    break;
            }
        }       
    private void OnCollisionEnter(Collision collusion)  // ������ "Castle" ���޽� ���ʹ� ���� �������� ����
    {
        if (collusion.transform.name == "Castle")
        {
            Debug.Log($"{ _health}");
            Debug.Log("����");
            enemyState = EnemyState.attack;
        }
        return;
    }
    public void OnTakePlayerDamage(float _dmg)   //�÷��̾� ��ũ��Ʈ���� ȣ���Ͽ� ���ʹ� ü�� ����
    {
        _health -= _dmg;
        if (_health <= 0)
        {
            Destroy(this.gameObject);
        }
        else return;
    }
    IEnumerator AttackCastle () //Castle ���� �ڷ�ƾ
    {
        attackAble = false;
        yield return new WaitForSeconds(1.6f);
        //GameManager.Instance.OnTakeDamage(enemyDamage); // ���� �߰�
        Debug.Log("����");
        attackAble = true;
    }


}
