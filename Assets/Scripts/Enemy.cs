using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType {walkenemy,runenemy,bossenemy}
enum EnemyState {move,attack}
public class Enemy : MonoBehaviour
{
    // EnemyType enemyType = EnemyType.walkenemy; 
    //  EnemyState enemyState = EnemyState.move;  -> 미사용으로 주석처리 - LJS
    [SerializeField] float _health;
    GameObject target;
    NavMeshAgent agent;
    private float enemyDamage;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetStatus(EnemyType.walkenemy, GameObject.Find("Castle"));
        agent.destination = target.transform.position;
        
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
                agent.speed = 15f;
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
        }
        return;
    }
    private void EnemyCastleDamage() 
    {
        //GameObject Castlehealth = GameObject.Find("Castle");
        //Castlehealth.
        //Debug.Log($"{_Castlehealth.GetComponent<Castle>()}");
       // GameManager.Instance.CastleDamage();
    }
    public void EnemyPlayerDamage() 
    {

    }


}
