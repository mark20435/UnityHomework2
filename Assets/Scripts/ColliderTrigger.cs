using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            //���a�I��v�ܩi�N���åv�ܩi
            gameObject.SetActive(false);

            List<List<GameObject>> deadList = GameManager.Instance().deadList;
            List<List<GameObject>> aliveList = GameManager.Instance().aliveList;
            if (aliveList.Count > 0)
            {
                for (int i = 0; i < aliveList.Count; i++)
                {
                    List<GameObject> currentAliveObjectList = aliveList[i];
                    List<GameObject> currentDeadObjectList = deadList[i];
                    for (int j = 0; j < currentAliveObjectList.Count; j++)
                    {
                        if (currentAliveObjectList[j].Equals(gameObject))
                        {
                            //�qAliveList���X���ê��v�ܩi�A�s�W��DeadList
                            currentAliveObjectList.Remove(gameObject);
                            currentDeadObjectList.Add(gameObject);
                            GameManager.Instance().score++;
                            UIGame.Instance().score.SetText($"Score:{GameManager.Instance().score}");
                            return;
                        }
                    }
                }
            }
        }
    }
}
