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
        castle.SetData(gameInfo.castleHP);//3¹ø

        //¸Ê.. Load ÇØÁÖ°í 
        //¸Ê .. AI °¡ °É¾î´Ù´Ò¼öÀÖ°Ô²û Bake ÇØÁà¾ßÇÔ

    }

    public void CheckGoal()
    {

    }

    private void LoadData()
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
