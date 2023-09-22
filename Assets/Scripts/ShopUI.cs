using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    Player player;
    Castle castle;

    //public Text val_curmoney;   //  ������
    public Text val_m_atkSpeed; //  ���� ���� ���
    public Text val_m_atkDamage;   //  ���ݷ� ���� ���
    public Text val_m_RepairCastle; //  ���� ���� ���
    //public Text val_m_Magic;    // ����(���ݱ�) ���
    int money_atkSpeed = 20;
    int money_atkDamage = 20;
    int money_RepairCastle = 50;
   // int money_Magic = 30;

    //[SerializeField] Transform effect_magicHit;

    //  ��ư ��ü Ȱ��ȭ/��Ȱ��ȭ
    public Button btn_atkSpeed;
    public Button btn_damage;
    public Button btn_RepairCastle;
   // public Button btn_Magic;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        castle = GameObject.Find("Castle").GetComponent<Castle>();
    }

    void Show_ShopInfo()
    {
        btn_damage.interactable = GameManager.Instance.IsBuy(money_atkDamage);
        btn_atkSpeed.interactable = GameManager.Instance.IsBuy(money_atkSpeed);
        btn_RepairCastle.interactable = GameManager.Instance.IsBuy(money_RepairCastle);
        //btn_Magic.interactable = GameManager.Instance.IsBuy(money_Magic);
    }



    public void OnShop()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }
    public void OffShop()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    public void BuyDamageUp()
    {
        if (GameManager.Instance.BuyItem(money_atkDamage))
        {
            float dmg = player.GetAttackDmg();
            player.SetAttackData(dmg + 0.5f);
            //money_atkDamage = 1000;
        }
    }
    public void BuyAttackSpeed()
    {
        if (transform.GetComponent<GameManager>().BuyItem(money_atkSpeed))
        {
            float speed = player.GetAttackSpeed();
            player.SetAttackSpeed(speed + 0.5f);
        }
    }

    public void BuyRepairCastle()
    {
        if (transform.GetComponent<GameManager>().BuyItem(money_RepairCastle))
        {
            
        }
    }


    //public void Go_Magic()
    //{
    //    if (transform.GetComponent<GameManager>().BuyItem(money_Magic))
    //    {

    //    }
    //}
}
