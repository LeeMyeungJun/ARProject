using System;
using UnityEngine;

public class GameManager : MonoSingle<GameManager>
{
    GameInfo gameInfo;
    LevelInfo levelInfo;
    Player player;
    Castle castle;
    EnemyWaveConfig config;

    int monsterCnt = 0;
    int curmoney = 0;
    public int Money  { get { return curmoney; } }

    EnemySpanwer enemySpanwer;
    public void AttackCastle(float _dmg)
    {
        castle.OnTakeDamage(_dmg);
    }
    override protected void Awake()
    {
        base.Awake();
        Init();
    }


    private void Init()
    {
        LoadData();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        castle = GameObject.FindWithTag("Castle").GetComponent<Castle>();
        enemySpanwer = GameObject.FindWithTag("Spawner").GetComponent<EnemySpanwer>();
        enemySpanwer.Setup(config);
        monsterCnt = (gameInfo.currLv * 3) + 5;
        player.SetData(gameInfo.atkSpeed, gameInfo.atkDmg);
        castle.SetData(gameInfo.castleHP);//3번
        curmoney = gameInfo.curmoney;

        SaveData(gameInfo.currLv);
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

        config = Resources.Load<EnemyWaveConfig>("Config/"+gameInfo.currLv);
    }
  
    void LoadMapData(int lv)
    {
        string path = "JsnLevels/" + "map_" + lv;
        if (Util.IsTextAsset(path) == false)// 진행할스테이지가없음 다깸
        {
            LoadingSceneController.LoadScene("ClearScene");
            return;
        }
        TextAsset textAsset = Util.LoadTextAsset(path);
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

        Debug.Log("GameScene: SAVEDATA  LV :" + lv);
    }

    public void DieEnemy(int _money)
    {
        curmoney += _money;
        monsterCnt--;
        if (monsterCnt == 0)
        {
            string path = "JsnLevels/" + "map_" + (gameInfo.currLv + 1).ToString();
            if (Util.IsTextAsset(path) == false)// 진행할스테이지가없음 다깸
            {
                LoadingSceneController.LoadScene("ClearScene");
            }
            else
            {
                SaveData(gameInfo.currLv + 1);
                LoadingSceneController.LoadScene("GameScene");
            }
        }
    }

    public void GameOver()
    {
        LoadingSceneController.LoadScene("TitleScene");
    }
}
