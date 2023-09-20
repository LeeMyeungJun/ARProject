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
    [SerializeField] float _health;
    GameObject target;
    NavMeshAgent agent;
    private float enemyDamage;
    private bool attackAble = false;
    
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.transform.position;

    }
    private void Update()
    {
        if (enemyState == EnemyState.move)
        {
            SetStatus(EnemyType.walkenemy, GameObject.Find("Castle"));
        }
        else if (enemyState == EnemyState.attack)
        {
            attackAble = true;
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
            Debug.Log("µµÂø");
            enemyState = EnemyState.attack;
        }
        return;
    }
    private void EnemyCastleDamage() 
    {
        //GameObject Castlehealth = GameObject.Find("Castle");
        //Castlehealth.
        //Debug.Log($"{_Castlehealth.GetComponent<Castle>()}");
       // GameManager.Instance.CastleDamage(); - GameManager¿¡ ¹­ÀÌ¸é Ãß°¡
    }
    public void EnemyPlayerDamage() 
    {

    }
    IEnumerator AttackCastle ()
    {
        attackAble = false;
        Debug.Log("°ø°Ý");
        // GameManager.Instance.CastleDamage(enemyDamage);
        yield return new WaitForSeconds(1.6f);
        attackAble = true;
    }


}
