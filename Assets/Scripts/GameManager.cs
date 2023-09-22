using System;
using UnityEngine;

public class GameManager : MonoSingle<GameManager>
{
    GameInfo gameInfo;
    LevelInfo levelInfo;
    Player player;
    private Castle castle;

    int monsterCnt = 0;
    int curmoney = 0;

    EnemySpanwer enemySpanwer;
    public void AttackCastle(float _dmg)
    {
        castle.OnTakeDamage(_dmg);
    }
    private void Awake()
    {
        Init();
    }


    private void Init()
    {
        LoadData();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        castle = GameObject.FindWithTag("Castle").GetComponent<Castle>();
        enemySpanwer = GameObject.FindWithTag("Spawner").GetComponent<EnemySpanwer>();
        enemySpanwer.Setup((gameInfo.currLv * 3) + 5);
        monsterCnt = (gameInfo.currLv * 3) + 5;
        player.SetData(gameInfo.atkSpeed, gameInfo.atkDmg);
        castle.SetData(gameInfo.castleHP);//3번
        curmoney = gameInfo.curmoney;
        //맵.. Load 해주고 
        //맵 .. AI 가 걸어다닐수있게끔 Bake 해줘야함

    }

    public bool IsBuy(int _Price)
    {
        if (curmoney >= _Price)
        {
            return true; //구매성공
        }
        return false; // 구매실패
    }


    public bool BuyItem(int _Price)
    {
        if (curmoney >= _Price)
        {
            curmoney -= _Price;
            return true; //구매성공
        }
        return false; // 구매실패
    }

    void LoadData()
    {
        if (gameInfo == null)
            gameInfo = new GameInfo();

        gameInfo = Util.LoadData<GameInfo>("/save.dat");
        LoadMapData(gameInfo.currLv);
    }
  
    void LoadMapData(int lv)
    {
        TextAsset textAsset = Util.LoadTextAsset("JsnLevels/" + "map_" + lv);
        levelInfo = new LevelInfo();
        Util.LoadJsonData<LevelInfo>(textAsset,out levelInfo);
        levelInfo.LoadData();//여기서 맵로드함

    }
    private void SaveData(int lv)
    {
        if (gameInfo == null)
            return;
        gameInfo.castleHP = castle.getHp();
        gameInfo.curmoney = this.curmoney;
        gameInfo.atkSpeed = player.GetAttackSpeed();
        gameInfo.atkDmg = player.GetAttackDmg();
        gameInfo.currLv = lv;
        Util.SaveData<GameInfo>(gameInfo,"/save.dat");
    }

    public void DieEnemy()
    {
        monsterCnt--;
        if (monsterCnt == 0)
        {
            //승리
            SaveData(gameInfo.currLv + 1);
            LoadingSceneController.LoadScene("GameScene");
        }
    }

    public void GameOver()
    {
        LoadingSceneController.LoadScene("TitleScene");
    }
}
