using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float atkDmg = 5.0f;
    float atkSpeed = 1.0f;
    [SerializeField] Transform effect_hit;    // 데미지 효과
    float delay = 0;

    [SerializeField] Animation anim;

    void AttackEnemy()
    {
#if UNITY_EDITOR
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit other;
        if (Physics.Raycast(ray, out other, 100f))
        {
            if (other.transform.tag == "Enemy")
            {
                SoundPlayer.PlaySoundFx("player_attack");
                anim.Play("StaffAction");
                    delay = 0;
                    if(effect_hit != null)
                    {
                        Instantiate(effect_hit, other.point, Quaternion.identity);
                    }
                    other.transform.GetComponent<Enemy>().OnTakePlayerDamage(atkDmg);
            }
        }
#else
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f,0.5f));
        RaycastHit other;
        if (Physics.Raycast(ray, out other, Camera.main.farClipPlane))
        {
            if (other.transform.tag == "Enemy")
            {
                SoundPlayer.PlaySoundFx("player_attack");
                anim.Play("StaffAction");
                delay = 0;
                if(effect_hit != null)
                {
                    Instantiate(effect_hit, other.point, Quaternion.identity);
                }
                other.transform.GetComponent<Enemy>().OnTakePlayerDamage(atkDmg);
            }
        }

#endif
    }

    private void Update()
    {
        delay += Time.deltaTime;
#if UNITY_EDITOR
        if (delay > atkSpeed)
        {

            if(Input.GetMouseButtonDown(0))
                AttackEnemy();
        }
#endif

    }

    public void OnAttackBtn()
    {
        if (delay > atkSpeed)
            AttackEnemy();
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
