using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum make_state {init, spawn,end}
public class EnemySpanwer : MonoBehaviour
{
    Transform enemyPrefab;

    EnemyWaveConfig waveConfig;

    private float waitSpawnTime = 0;
    private int makeCnt = 0;
    private bool isSpawnAble = true;
    
    make_state spawnState = make_state.init;

    //private int maxEnemy = 0;

    private void Awake()
    {
        enemyPrefab = Resources.Load<Transform>("Prefabs/Enemy");
    }
    public void Setup(EnemyWaveConfig _waveConfig)
    {
        //maxEnemy = _num;
        waveConfig = _waveConfig;
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
        if (makeCnt == waveConfig.spawnCnt) return true;
        //if (makeCnt == maxEnemy) return true;
        else return false;
    }
    //IEnumerator MakeEnemy() 
    //{
    //    isSpawnAble = false;
    //    yield return new WaitForSeconds(2f);
    //    Transform obj = Instantiate(this.pos[0], this.transform.position, this.transform.rotation);
    //    obj.GetComponent<Enemy>().SetStatus(EnemyType.runenemy);

    //    isSpawnAble = true;
    //    this.makeCnt += 1;
    //}

    IEnumerator MakeEnemy()
    {
        isSpawnAble = false;
        yield return new WaitForSeconds(2f);
        this.makeCnt += 1;

        Transform obj = Instantiate(this.enemyPrefab, this.transform.position, this.transform.rotation);
        if (makeCnt == waveConfig.spawnCnt && waveConfig.isBoss)//보스 생성
        {
            obj.GetComponent<Enemy>().SetStatus(EnemyType.bossenemy);
        }
        else 
        {
            int ran = Random.Range(0, waveConfig.enemys.Length);
            obj.GetComponent<Enemy>().SetStatus(waveConfig.enemys[ran]);
        }

        isSpawnAble = true;
    }
}
