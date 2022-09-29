using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //GameManager用singleton的方式處理
    private static GameManager mInstance;
    public static GameManager Instance() { return mInstance; }
    SceneLoader sceneLoader = null;
    private void Awake()
    {
        mInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //取得sceneLoader元件
        sceneLoader = GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 換場景
    /// </summary>
    /// <param name="sceneName">場景名稱</param>
    public void ChangeScene(string sceneName)
    {
        //呼叫sceneLoader的函式非同步換場景
        StartCoroutine(sceneLoader.ChangeSceneAsync(sceneName));
    }
}
