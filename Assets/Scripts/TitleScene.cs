using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public void GoGameScene()
    {
        GameInfo data = new GameInfo(); // 공간을 만들어줌 .
        data.castleHP = 500;
        Util.SaveData<GameInfo>(data, "/save.dat");
        LoadingSceneController.LoadScene("GameScene");
    }

    public void Btn_LoadGame()
    {
        LoadData();
        GoGameScene();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void LoadData()
    {
        GameInfo data = new GameInfo(); // 공간을 만들어줌 .
        data = Util.LoadData<GameInfo>("/save.dat");
    }

    void SaveData()
    {
        GameInfo data = new GameInfo();
        data.atkSpeed = 2.0f;
        data.atkDmg = 50.0f;
        Util.SaveData<GameInfo>(data, "/save.dat");
    }
}
