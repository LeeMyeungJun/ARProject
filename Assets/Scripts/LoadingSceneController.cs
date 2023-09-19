using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] Image progressBar;
    
    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    /// <summary>
    /// 호출 시 LoadingScene으로 씬전환. LoadingSceneController가 Start()되면서 LoadScene 코루틴 생성
    /// </summary>
    /// <param name="sceneName">전환 대상 씬</param>
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount == 1.0f)
                {
                    yield return new WaitForSeconds(0.5f);
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}