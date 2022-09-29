using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// �ΫD�P�B���覡������
    /// </summary>
    /// <param name="sceneName">�����W��</param>
    /// <returns></returns>
    public IEnumerator ChangeSceneAsync(string sceneName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        yield return ao;
    }
}
