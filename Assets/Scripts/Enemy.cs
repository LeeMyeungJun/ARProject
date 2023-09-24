using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum EnemyType {walkenemy,runenemy,bossenemy}  // ���ʹ� Ÿ�� ���� 
enum EnemyState {move,attack} // ���ʹ� ���� ���� (move �� ���ʹ� Ÿ�� ������ ���, attack�� ���ݿ� ���)
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
        agent.destination = target.transform.position;   // ���ʹ��� Ÿ�� ������ ����.
    }
    void Update()
    {
        if (enemyState == EnemyState.attack && attackAble == true)  // ���ʹ� ���°� ����, attackAble�� Ʈ�� �϶� ���� ����  => �ڷ�ƾ ����
        {
            StartCoroutine("AttackCastle"); 
        }
    }
    public void SetStatus(EnemyType _type)  // �ش� �Լ��� �������� �� ���ʹ� ����
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
    private void OnCollisionEnter(Collision collusion)  // ������ "Castle" ���޽� ���ʹ� ���� �������� ����
    {
        if (collusion.transform.tag == "Castle")
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
        hpBar.value = _health;
        if (_health <= 0)
        {
            GameManager.Instance.DieEnemy(money);
            Destroy(this.gameObject);
        }
        else return;
    }
    IEnumerator AttackCastle() //Castle ���� �ڷ�ƾ
    {
        attackAble = false;
        yield return new WaitForSeconds(1.6f);
        //GameManager.Instance.OnTakeDamage(enemyDamage); // ���� �߰�
        Debug.Log("����");
        attackAble = true;
        anim.SetTrigger("attack");
        GameManager.Instance.AttackCastle(enemyDamage);
    }


}
