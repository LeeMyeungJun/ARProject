using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    private  float health = 1000;

    public void OnTakeDamage(float _dmg)
    {
         health -= _dmg;

        if (health <= 0)
            GameManager.Instance.GameOver();
    }
    public float getHp()
    {
        return health;
    }
    public void SetData(float _health) 
    {
         health = _health;
    }
}
