using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //GameManager用singleton的方式處理
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
        //取得sceneLoader元件
        sceneLoader = GetComponent<SceneLoader>();
        objectPool = GetComponent<ObjectPool>();
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
