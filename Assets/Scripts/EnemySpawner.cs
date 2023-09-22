using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum make_state {init, spawn,end}
public class EnemySpanwer : MonoBehaviour
{
    [SerializeField] private Transform[] pos;
    private float waitSpawnTime = 0;
    private int makeEnermyNum = 0;
    private bool isSpawnAble = true;
    
    make_state spawnState = make_state.init;

    private void Start()
    {
        
    }
    void Update()
    {
        EnermySpawn();
    }
    private void EnermySpawn()
    {
        switch (spawnState)
        {
            case make_state.init:
                waitSpawnTime += Time.deltaTime;
                if (waitSpawnTime >= 2f)
                {
                    waitSpawnTime = 0;
                    spawnState = make_state.spawn;
                }
                break;
            case make_state.spawn:
                if (IsMakeEnd() == true)
                {
                    spawnState = make_state.end;
                    return;
                }
                else if (isSpawnAble == true)
                {
                    StartCoroutine("MakeEnemy");
                }
                break;
            case make_state.end:
                break;
        }
    }
    bool IsMakeEnd()
    {
        if (makeEnermyNum == 5) return true;
        else return false;
    }
    IEnumerator MakeEnemy() 
    {
        isSpawnAble = false;
        yield return new WaitForSeconds(2f);
        Instantiate(this.pos[0], this.transform.position, this.transform.rotation);
        isSpawnAble = true;
        this.makeEnermyNum += 1;
    }
}
