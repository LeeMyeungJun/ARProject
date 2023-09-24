using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public void GoGameScene()
    {
        GameInfo data = new GameInfo(); // 공간을 만들어줌 .
        Util.SaveData<GameInfo>(data, "/save.dat");
        LoadingSceneController.LoadScene("GameScene");
    }


    public void Btn_LoadGame()
    {
        LoadingSceneController.LoadScene("GameScene");
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


}
