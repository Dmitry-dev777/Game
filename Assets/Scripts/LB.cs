using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LB : MonoBehaviour
{
    string[] links = new string[3] { "https://unityplatformergame.000webhostapp.com/level[0].unity3d",
        "https://unityplatformergame.000webhostapp.com/level[1].unity3d",
        "https://unityplatformergame.000webhostapp.com/level[2].unity3d" };


    private int version = 0;
    public string[] check;
    public string[] check1;
    public string[] check2;

    public void OnClickGameDownload()
    {
        StartCoroutine(DownloadAndCache());
    }

    IEnumerator DownloadAndCache()
    {
        while (!Caching.ready)
            yield return null;
        var www = WWW.LoadFromCacheOrDownload(links[0], version);
        var www1 = WWW.LoadFromCacheOrDownload(links[1], version);
        var www2 = WWW.LoadFromCacheOrDownload(links[2], version);
        yield return www;
        yield return www1;
        yield return www2;

        var assetBundle = www.assetBundle;
        var assetBundle1 = www1.assetBundle;
        var assetBundle2 = www2.assetBundle;

        check = assetBundle.GetAllScenePaths();
        check1 = assetBundle1.GetAllScenePaths();
        check2 = assetBundle2.GetAllScenePaths();
        if (HeroController.Player_Level != 2 || HeroController.Player_Level != 3)
        {
            SceneManager.LoadSceneAsync("Level[0]");
        }
    }
}