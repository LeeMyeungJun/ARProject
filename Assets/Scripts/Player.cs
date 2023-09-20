using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float atkDmg = 5.0f;
    float atkSpeed = 0.1f;
    public Transform effect_hit;    // ������ ȿ��
    public float delay; // ���ݼӵ� ������

    void AttackEnemy()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit other;
            if (Physics.Raycast(ray, out other, 100f))
            {
                if (other.transform.tag == "Enemy")
                {
                    Instantiate(effect_hit, other.point, Quaternion.identity);
                    //other.transform.GetComponent<Enemy>().      (GameInfo.attackDmg);
                }
            }
        }
    }

    void Btn_Atk()
    {
        //delay = GameObject.Find("GameInfo").GetComponent<attackDmg>;
    }

    public void SetData(float _speed, float _dmg)
    {
        atkDmg = _dmg;
        atkSpeed = _speed;
    }

    public float GetAttackSpeed() { return atkSpeed; }
    public float GetAttackDmg() { return atkDmg; }
}
