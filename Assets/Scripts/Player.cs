using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Transform effect_hit;    // 데미지 효과
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


}
