using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    Player player;
    Castle castle;

    [SerializeField] private GameObject shopUI;
    //public Text val_curmoney;   //  ������
    public Text val_m_atkSpeed; //  ���� ���� ���
    public Text val_m_atkDamage;   //  ���ݷ� ���� ���
    public Text val_m_RepairCastle; //  ���� ���� ���
    public Text val_m_Magic;    // ����(���ݱ�) ���
    int money_atkSpeed;
    int money_atkDamage;
    int money_RepairCastle;
    int money_Magic;

    [SerializeField] Transform effect_magicHit;

    //  ��ư ��ü Ȱ��ȭ/��Ȱ��ȭ
    public Button btn_atkSpeed;
    public Button btn_damage;
    public Button btn_RepairCastle;
    public Button btn_Magic;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        castle = GameObject.Find("Castle").GetComponent<Castle>();
        //  ���� ui off
        shopUI.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    void Show_ShopInfo()
    {
        btn_damage.interactable = GameManager.Instance.BuyItem(money_atkDamage);
        btn_atkSpeed.interactable = GameManager.Instance.BuyItem(money_atkSpeed);
        btn_RepairCastle.interactable = GameManager.Instance.BuyItem(money_RepairCastle);
        btn_Magic.interactable = GameManager.Instance.BuyItem(money_Magic);
    }



    public void Btn_shopopen()
    {
        Time.timeScale = 0f;
        shopUI.gameObject.SetActive(true);
    }
    public void Btn_shopclose()
    {
        Time.timeScale = 1f;
        shopUI.gameObject.SetActive(false);
    }
    public void Go_damage()
    {
        if (GameManager.Instance.BuyItem(money_atkDamage))
        {
            float dmg = player.GetAttackDmg();
            player.SetAttackData(dmg + 0.5f);
            //money_atkDamage = 1000;
        }
    }
    public void Go_atkSpeed()
    {
        if (transform.GetComponent<GameManager>().BuyItem(money_atkSpeed))
        {
            float speed = player.GetAttackSpeed();
            player.SetAttackSpeed(speed + 0.5f);
        }
    }

    public void Go_RepairCastle()
    {
        if (transform.GetComponent<GameManager>().BuyItem(money_RepairCastle))
        {
            
        }
    }

    public void Go_Magic()
    {
        if (transform.GetComponent<GameManager>().BuyItem(money_Magic))
        {
            //GameObject enemy = GameObject.FindWithTag("Enemy");
            //Instantiate(effect_magicHit, enemy.transform.position, Quaternion.identity);
            //Destroy(GameObject.FindWithTag("Enemy"));
        }
    }
}
