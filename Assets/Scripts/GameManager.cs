using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //GameManager��singleton���覡�B�z
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
        //���osceneLoader����
        sceneLoader = GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="sceneName">�����W��</param>
    public void ChangeScene(string sceneName)
    {
        //�I�ssceneLoader���禡�D�P�B������
        StartCoroutine(sceneLoader.ChangeSceneAsync(sceneName));
    }
}
