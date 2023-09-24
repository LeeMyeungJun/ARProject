using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum EnemyType {walkenemy,runenemy,bossenemy}  // 에너미 타입 지정 
enum EnemyState {move,attack} // 에너미 상태 지정 (move 는 에너미 타입 지정에 사용, attack은 공격에 사용)
public class Enemy : MonoBehaviour
{
    EnemyState enemyState = EnemyState.move;  
    GameObject target;
    NavMeshAgent agent;
    private float _health;
    private float enemyDamage;
    private int money;
    private bool attackAble = true;
    Slider hpBar;

    
    [SerializeField] GameObject[] models;
    Animator anim;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        hpBar = GetComponentInChildren<Slider>();
    }
    void Start()
    {
        target = GameObject.FindWithTag("Castle");
        agent.destination = target.transform.position;   // 에너미의 타겟 목적지 지정.
    }
    void Update()
    {
        if (enemyState == EnemyState.attack && attackAble == true)  // 에너미 상태가 공격, attackAble이 트루 일때 공격 진행  => 코루틴 진행
        {
            StartCoroutine("AttackCastle"); 
        }
    }
    public void SetStatus(EnemyType _type)  // 해당 함수로 스테이지 별 에너미 세팅
    {
            switch (_type)
            {
                case EnemyType.walkenemy:
                    agent.speed = 1f;
                    _health = 100f;
                    enemyDamage = 10;
                    money = 50;
                    break;
                case EnemyType.runenemy:
                    agent.speed = 2f;
                    _health = 200f;
                    enemyDamage = 20;
                    money = 50;
                    break;
                case EnemyType.bossenemy:
                    agent.speed = 2f;
                    _health = 1000f;
                    enemyDamage = 50;
                    money = 50;
                    break;
            }


        models[(int)_type].SetActive(true);
        anim = models[(int)_type].GetComponent<Animator>();
        hpBar.maxValue = _health;
        hpBar.value = _health;

    }
    private void OnCollisionEnter(Collision collusion)  // 목적지 "Castle" 도달시 에너미 상태 공격으로 변경
    {
        if (collusion.transform.tag == "Castle")
        {
            Debug.Log($"{ _health}");
            Debug.Log("도착");
            enemyState = EnemyState.attack;
        }
        return;
    }
    public void OnTakePlayerDamage(float _dmg)   //플레이어 스크립트에서 호출하여 에너미 체력 감소
    {
        _health -= _dmg;
        hpBar.value = _health;
        if (_health <= 0)
        {
            GameManager.Instance.DieEnemy(money);
            Destroy(this.gameObject);
        }
        else return;
    }
    IEnumerator AttackCastle() //Castle 공격 코루틴
    {
        attackAble = false;
        yield return new WaitForSeconds(1.6f);
        //GameManager.Instance.OnTakeDamage(enemyDamage); // 추후 추가
        Debug.Log("공격");
        attackAble = true;
        anim.SetTrigger("attack");
        GameManager.Instance.AttackCastle(enemyDamage);
    }


}
