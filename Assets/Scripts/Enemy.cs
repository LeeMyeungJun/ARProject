using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType {walkenemy,runenemy,bossenemy}
enum EnemyState {move,attack}
public class Enemy : MonoBehaviour
{
    EnemyState enemyState = EnemyState.move;  
    float _health;
    GameObject target;
    NavMeshAgent agent;
    private float enemyDamage;
    private bool attackAble = true;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        SetStatus(EnemyType.runenemy, GameObject.Find("Castle"));
        agent.destination = target.transform.position;
    }
    void Update()
    {
        if (enemyState == EnemyState.attack && attackAble == true)
        {
            StartCoroutine("AttackCastle");
        }
    }
        public void SetStatus(EnemyType _type, GameObject _target)
    {
        target = _target;
       
            switch (_type)
            {
                case EnemyType.walkenemy:
                    agent.speed = 5f;
                    _health = 100f;
                    enemyDamage = 10;
                    break;
                case EnemyType.runenemy:
                    agent.speed = 10f;
                    _health = 200f;
                    enemyDamage = 20;
                    break;
                case EnemyType.bossenemy:
                    agent.speed = 20f;
                    _health = 300f;
                    enemyDamage = 50;
                    break;
            }
        }       
    private void OnCollisionEnter(Collision collusion)
    {
        if (collusion.transform.name == "Castle")
        {
            Debug.Log($"{ _health}");
            Debug.Log("도착");
            enemyState = EnemyState.attack;
        }
        return;
    }
    public void OnTakePlayerDamage() 
    {
        //_health -= //플레이어 무기 데이지
        if (_health <= 0)
        {
            Destroy(this.gameObject);
        }
        else return;
    }
    IEnumerator AttackCastle ()
    {
        attackAble = false;
        yield return new WaitForSeconds(1.6f);
        //GameManager.Instance.OnTakeDamage(enemyDamage); // 추후 추가
        Debug.Log("공격");
        attackAble = true;
    }


}
