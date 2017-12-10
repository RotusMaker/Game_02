using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFrame : MonoSingleton<MainFrame> {

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
        // 화면해상도 1024로 전부 맞춤. 비율축소. 우리게임은 세로만 사용.
        if (Screen.currentResolution.height > 1024)
        {
            float fRate = System.Convert.ToSingle(Screen.currentResolution.width) / 1024f;
            Screen.SetResolution(System.Convert.ToInt32(Screen.currentResolution.width * fRate), 1024, true);
        }

        // 35프레임으로 맞춤. 넘 밀려보이면 늘리기. 발열에 도움됨.
        Application.targetFrameRate = 35;
    }

    // 원하는 씬으로 이동.
    public void NextScene(int number, float delay = 0f)
    {
        StartCoroutine(GoNextScene(number, delay));
    }
    IEnumerator GoNextScene(int number, float delay)
    {
        if (delay > 0f)
        {
            yield return new WaitForSeconds(delay);
        }
        string nextScene = MainFrame.instance.GetSceneName(number);
        AsyncOperation oper = Application.LoadLevelAsync(nextScene);
        yield return oper;
    }

    // 씬이름 리턴함.
    public string GetSceneName(int number)
    {
        string sceneName = string.Empty;
        switch(number)
        {
            case 0:     sceneName = "Scene000Common"; break;
			case 1:     sceneName = "Scene001Load"; break;
			case 2:     sceneName = "Scene002NickName"; break;
            case 10:    sceneName = "Scene010RefereeIntroduce"; break;
            case 11:    sceneName = "Scene011GameRanking"; break;
            case 12:    sceneName = "Scene012Commentary"; break;
            case 13:    sceneName = "Scene013Chat"; break;
            case 100:   sceneName = "Scene100GameChoice"; break;
            case 101:   sceneName = "Scene101DualGame"; break;
        }

        return sceneName;
    }
}
