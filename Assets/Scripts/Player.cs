using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float atkDmg = 5.0f;
    float atkSpeed = 1.0f;
    [SerializeField] Transform effect_hit;    // 데미지 효과
    float delay = 0;

    void AttackEnemy()
    {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit other;
            if (Physics.Raycast(ray, out other, 100f))
            {
                if (other.transform.tag == "Enemy")
                {
                        delay = 0;
                        if(effect_hit != null)
                            Instantiate(effect_hit, other.point, Quaternion.identity);
                        other.transform.GetComponent<Enemy>().OnTakePlayerDamage(atkDmg);
                }
            }
    }

    private void Update()
    {
        delay += Time.deltaTime;
        if(delay > atkSpeed && Input.GetMouseButtonDown(0) )
        {
            //delay = 0;
            AttackEnemy();
        }
    }

    public void SetAttackSpeed(float _speed) { atkSpeed = _speed; }
    public void SetAttackData(float _dmg) { atkDmg = _dmg; }
    public void SetData(float _speed, float _dmg)
    {
        atkDmg = _dmg;
        atkSpeed = _speed;
    }

    public float GetAttackSpeed() { return atkSpeed; }
    public float GetAttackDmg() { return atkDmg; }
}
