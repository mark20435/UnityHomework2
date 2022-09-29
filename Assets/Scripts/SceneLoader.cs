using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// 用非同步的方式換場景
    /// </summary>
    /// <param name="sceneName">場景名稱</param>
    /// <returns></returns>
    public IEnumerator ChangeSceneAsync(string sceneName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        yield return ao;
    }
}
