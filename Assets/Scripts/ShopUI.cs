using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    Player player;
    Castle castle;

    public Text val_curmoney;   //  소지금
    public Text val_m_atkSpeed; //  공속 증가 비용
    public Text val_m_atkDamage;   //  공격력 증가 비용
    public Text val_m_RepairCastle; //  성벽 수리 비용
    //public Text val_m_Magic;    // 마법(폭격기) 비용
    int money_atkSpeed = 20;
    int money_atkDamage = 20;
    int money_RepairCastle = 50;
   // int money_Magic = 30;

    //[SerializeField] Transform effect_magicHit;

    //  버튼 객체 활성화/비활성화
    public Button btn_atkSpeed;
    public Button btn_damage;
    public Button btn_RepairCastle;
   // public Button btn_Magic;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        castle = GameObject.FindWithTag("Castle").GetComponent<Castle>();

        val_m_atkSpeed.text = money_atkSpeed.ToString();
        val_m_atkDamage.text = money_atkDamage.ToString();
        val_m_RepairCastle.text = money_RepairCastle.ToString();
    }

    private void OnEnable()
    {
        UpdateShopInfo();
    }

    void UpdateBtnIntractable()
    {
        btn_damage.interactable = GameManager.Instance.IsBuy(money_atkDamage);
        btn_atkSpeed.interactable = GameManager.Instance.IsBuy(money_atkSpeed);
        btn_RepairCastle.interactable = GameManager.Instance.IsBuy(money_RepairCastle);
        //btn_Magic.interactable = GameManager.Instance.IsBuy(money_Magic);
    }

    void UpdateShopInfo()
    {
        val_curmoney.text = GameManager.Instance.Money.ToString();

        UpdateBtnIntractable();
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
            UpdateShopInfo();
            float dmg = player.GetAttackDmg();
            player.SetAttackData(dmg + 10f);
            //money_atkDamage = 1000;
        }
    }
    public void BuyAttackSpeed()
    {
        if (GameManager.Instance.BuyItem(money_atkSpeed))
        {
            UpdateShopInfo();
            float speed = player.GetAttackSpeed();
            player.SetAttackSpeed(speed - 0.05f);
        }
    }

    public void BuyRepairCastle()
    {
        if (GameManager.Instance.BuyItem(money_RepairCastle))
        {
            UpdateShopInfo();
            castle.RepairCastle();
        }
    }


    //public void Go_Magic()
    //{
    //    if (transform.GetComponent<GameManager>().BuyItem(money_Magic))
    //    {

    //    }
    //}
}
