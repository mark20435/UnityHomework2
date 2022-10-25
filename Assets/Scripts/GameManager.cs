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

    private List<ObjectPool.ObjectPoolData> poolDataList1 = null;
    private List<ObjectPool.ObjectPoolData> poolDataList2 = null;
    private List<ObjectPool.ObjectPoolData> poolDataList3 = null;
    private List<ObjectPool.ObjectPoolData> poolDataList4 = null;
    public List<List<ObjectPool.ObjectPoolData>> poolList = new List<List<ObjectPool.ObjectPoolData>>();
    private List<GameObject> aliveObjectList1 = new List<GameObject>();
    private List<GameObject> aliveObjectList2 = new List<GameObject>();
    private List<GameObject> aliveObjectList3 = new List<GameObject>();
    private List<GameObject> aliveObjectList4 = new List<GameObject>();
    public List<List<GameObject>> aliveList = new List<List<GameObject>>();
    private List<GameObject> deadObjectList1 = new List<GameObject>();
    private List<GameObject> deadObjectList2 = new List<GameObject>();
    private List<GameObject> deadObjectList3 = new List<GameObject>();
    private List<GameObject> deadObjectList4 = new List<GameObject>();
    public List<List<GameObject>> deadList = new List<List<GameObject>>();
    public Object npcObject;
    public int score = 0;

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

    // todo �ĤG���i�J�C���ɦA�I�s�@���|�X��
    IEnumerator SpawnObject(string sceneName)
    {
        int mobCount = 0;
        if (sceneName.Equals("GameScene"))
        {
            mobCount = 20;
            int poolDataListCount = 4;
            int mobPerPool = mobCount / poolDataListCount;
            poolDataList1 = objectPool.InitObjectPoolData(npcObject, mobPerPool);
            poolDataList2 = objectPool.InitObjectPoolData(npcObject, mobPerPool);
            poolDataList3 = objectPool.InitObjectPoolData(npcObject, mobPerPool);
            poolDataList4 = objectPool.InitObjectPoolData(npcObject, mobPerPool);
            poolList.Add(poolDataList1);
            poolList.Add(poolDataList2);
            poolList.Add(poolDataList3);
            poolList.Add(poolDataList4);
            aliveList.Add(aliveObjectList1);
            aliveList.Add(aliveObjectList2);
            aliveList.Add(aliveObjectList3);
            aliveList.Add(aliveObjectList4);
            deadList.Add(deadObjectList1);
            deadList.Add(deadObjectList2);
            deadList.Add(deadObjectList3);
            deadList.Add(deadObjectList4);

            for (int j = 0; j < poolDataListCount; j++)
            {
                List<ObjectPool.ObjectPoolData> currentPoolDataList = poolList[j];
                List<GameObject> currentAliveObjectList = aliveList[j];
                for (int i = 0; i < mobPerPool; i++)
                {
                    GameObject go = objectPool.LoadObjectFromPool(currentPoolDataList);
                    go.SetActive(true);
                    //���O�ͦ��v�ܩi��|�Ӥ��P���ϰ�
                    switch (j)
                    {
                        case 0:
                            go.transform.position = new Vector3(Random.Range(7.0f, 13.0f), 0, Random.Range(7.0f, 13.0f));
                            break;
                        case 1:
                            go.transform.position = new Vector3(Random.Range(-13.0f, -7.0f), 0, Random.Range(7.0f, 13.0f));
                            break;
                        case 2:
                            go.transform.position = new Vector3(Random.Range(7.0f, 13.0f), 0, Random.Range(-13.0f, -7.0f));
                            break;
                        case 3:
                            go.transform.position = new Vector3(Random.Range(-13.0f, -7.0f), 0, Random.Range(-13.0f, -7.0f));
                            break;
                        default:
                            break;
                    }
                    currentAliveObjectList.Add(go);
                }
            }

            //�C10��۰ʴ_���v�ܩi
            TimeManager.SetTimeEvent(10, Revive);
        }
        while (true)
        {
            int completeMob = 0;
            for (int i = 0; i < poolList.Count; i++)
            {
                completeMob += poolList[i].Count;
            }
            int count = completeMob;
            if (count == mobCount)
            {
                break;
            }
            yield return 0;
        }
    }

    //�_���v�ܩi
    private void Revive()
    {
        if (deadList.Count > 0 && aliveList.Count > 0)
        {
            for (int i = 0; i < deadList.Count; i++)
            {
                List<GameObject> currentAliveObjectList = aliveList[i];
                List<GameObject> currentDeadObjectList = deadList[i];
                if (currentDeadObjectList.Count > 0)
                {
                    for(int j = 0; j < currentDeadObjectList.Count; j++)
                    {
                        currentDeadObjectList[j].SetActive(true);
                        currentAliveObjectList.Add(currentDeadObjectList[j]);
                    }
                    currentDeadObjectList.Clear();
                }
            }
        }
    }
}
