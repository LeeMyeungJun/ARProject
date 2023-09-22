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
        castle.SetData(gameInfo.castleHP);//3��
        curmoney = gameInfo.curmoney;
        //��.. Load ���ְ� 
        //�� .. AI �� �ɾ�ٴҼ��ְԲ� Bake �������

    }

    public bool IsBuy(int _Price)
    {
        if (curmoney >= _Price)
        {
            return true; //���ż���
        }
        return false; // ���Ž���
    }


    public bool BuyItem(int _Price)
    {
        if (curmoney >= _Price)
        {
            curmoney -= _Price;
            return true; //���ż���
        }
        return false; // ���Ž���
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
        levelInfo.LoadData();//���⼭ �ʷε���

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
            //�¸�
            SaveData(gameInfo.currLv + 1);
            LoadingSceneController.LoadScene("GameScene");
        }
    }

    public void GameOver()
    {
        LoadingSceneController.LoadScene("TitleScene");
    }
}
