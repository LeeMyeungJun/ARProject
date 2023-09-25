using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{

    private readonly float maxHealth = 1000;
    private float health = 1000;
    Slider slider;
    [SerializeField] GameObject antiBakeObj;
    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        slider.maxValue = maxHealth;
    }
    private void Start()
    {
        antiBakeObj.SetActive(false);
    }
    public void OnTakeDamage(float _dmg)
    {
         health -= _dmg;
        slider.value = health;
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
        slider.value = health;
    }

    public void RepairCastle()
    {
        slider.value = maxHealth;
    }

}
