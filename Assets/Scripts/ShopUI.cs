using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    public Text val_curmoney;   //  ������
    public Text val_m_atkSpeed; //  ���� ���� ���
    public Text val_m_damage;   //  ���ݷ� ���� ���

    //  ��ư ��ü Ȱ��ȭ/��Ȱ��ȭ
    public Button btn_atkSpeed;
    public Button btn_damage;


    void Start()
    {
        //  ���� ui off
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
