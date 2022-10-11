using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private System.Action<string> afterSceneLoad;
    
    private void Start()
    {
        SceneManager.sceneLoaded += AfterSceneLoaded;
    }

    private void AfterSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (afterSceneLoad != null)
        {
            afterSceneLoad(scene.name);
        }
    }

    /// <summary>
    /// 用非同步的方式換場景
    /// </summary>
    /// <param name="sceneName">場景名稱</param>
    /// <returns></returns>
    public IEnumerator ChangeSceneAsync(string sceneName, System.Action<string> loadObject)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        if (ao == null)
        {
            yield break;
        }
        afterSceneLoad = loadObject;
        ao.allowSceneActivation = false;
        while (ao.isDone == false)
        {
            ao.allowSceneActivation = true;
            yield return 0;
        }
        
    }
}
