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
  //  EnemyState enemyState = EnemyState.move;
    [SerializeField] float _health;
    GameObject target;
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetStatus(EnemyType.walkenemy, GameObject.Find("Castle"));
        agent.destination = target.transform.position;
    }
    public void SetStatus(EnemyType _type,GameObject _target)
    {
        target = _target;
        switch (_type) 
        {
            case EnemyType.walkenemy:
                agent.speed = 5f;
                _health = 100f;
                break;
            case EnemyType.runenemy:
                agent.speed = 10f;
                _health = 200f;
                break;
            case EnemyType.bossenemy:
                agent.speed = 15f;
                _health = 300f;
                break;
        }
    }

    /*void EnemyMove()
    {
        switch (enemyState) 
        { 
            case EnemyState.move:
                agent.destination = target.transform.position;
                break;
            case EnemyState.attack:
                agent.destination = target.transform.position;
                break;
        }
    }*/
    
    private void OnCollisionEnter(Collision collusion)
    {
        if (collusion.transform.name == "Castle")
        {
            Debug.Log($"{ _health}");
            Debug.Log("µµÂø");
            //enemyState = EnemyState.attack;
        }
        return;
    }
}
