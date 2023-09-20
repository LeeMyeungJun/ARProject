using UnityEngine;

public class GameManager : MonoSingle<GameManager>
{
    GameInfo gameInfo;
    Player player;
    private Castle castle;

    int curmoney = 0;


    public void AttackCastle(float _dmg)
    {
        castle.OnTakeDamage(_dmg);
    }

    private void Init()
    {
        LoadData();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.SetData(gameInfo.atkSpeed, gameInfo.atkDmg);
        castle.SetData(gameInfo.castleHP);//3번
        curmoney = gameInfo.curmoney;
        //맵.. Load 해주고 
        //맵 .. AI 가 걸어다닐수있게끔 Bake 해줘야함

    }

    public bool BuyItem(int _Price)
    {
        if(curmoney >= _Price)
        {
            curmoney -= _Price;
            return true; //구매성공
        }
        return false; // 구매실패
    }
    public void CheckGoal()
    {

    }

    void LoadData()
    {
        if (gameInfo == null)
            gameInfo = new GameInfo();

        gameInfo = Util.LoadData<GameInfo>("/save.dat");
    }
  
    private void SaveData()
    {
        if (gameInfo == null)
            return;
        gameInfo.castleHP = castle.getHp();
        gameInfo.curmoney = this.curmoney;
        gameInfo.atkSpeed = player.GetAttackSpeed();
        gameInfo.atkDmg = player.GetAttackDmg();

        Util.SaveData<GameInfo>(gameInfo,"/save.dat");
    }
}
