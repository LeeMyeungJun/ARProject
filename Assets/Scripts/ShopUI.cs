using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    public Text val_curmoney;   //  소지금
    public Text val_m_atkSpeed; //  공속 증가 비용
    public Text val_m_damage;   //  공격력 증가 비용

    //  버튼 객체 활성화/비활성화
    public Button btn_atkSpeed;
    public Button btn_damage;


    void Start()
    {
        //  상점 ui off
        shopUI.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    void Show_ShopInfo()
    {

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
    public void Btn_damage()
    {

    }
    public void Btn_atkSpeed()
    {

    }
}
