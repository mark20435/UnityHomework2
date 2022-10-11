using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //GameManager��singleton���覡�B�z
    private static GameManager mInstance;
    public static GameManager Instance() { return mInstance; }
    SceneLoader sceneLoader = null;
    ObjectPool objectPool = null;

    private List<ObjectPool.ObjectPoolData> poolDataList = null;
    private List<GameObject> aliveObjectList = new List<GameObject>();
    public Object npcObject;

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
        objectPool = GetComponent<ObjectPool>();
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
        StartCoroutine(sceneLoader.ChangeSceneAsync(sceneName, ObjectLoading));
    }

    void ObjectLoading(string sceneName)
    {
        StartCoroutine(SpawnObject(sceneName));
    }

    IEnumerator SpawnObject(string sceneName)
    {
        int mobCount = 0;
        if (sceneName.Equals("GameScene"))
        {
            mobCount = 20;
            poolDataList = objectPool.InitObjectPoolData(npcObject, mobCount);

            for (int i = 0; i < mobCount; i++)
            {
                GameObject go = objectPool.LoadObjectFromPool(poolDataList);
                go.SetActive(true);
                go.transform.position = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-5.0f, 5.0f));
                aliveObjectList.Add(go);
            }
        }
        while (true)
        {
            int count = poolDataList == null ? 0 : poolDataList.Count;
            if (count == mobCount)
            {
                break;
            }
            yield return 0;
        }
    }
}
