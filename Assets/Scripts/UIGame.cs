using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    //UIGame��singleton���覡�B�z
    private static UIGame mInstance;
    public static UIGame Instance() { return mInstance; }
    public TMP_Text score;
    public TMP_Text time;
    public int sec = 60;
    public GameObject player;
    public GameObject result;
    public TMP_Text resultScore;

    private void Awake()
    {
        mInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        result.SetActive(false);
        time.SetText($"Time:{sec}");
        TimeManager.SetTimeEvent(1, CountDown);
    }

    private void CountDown()
    {
        if (sec > 0)
        {
            sec--;
            time.SetText($"Time:{sec}");
        }
        
        if(sec == 0)
        {
            sec = -1;
            resultScore.SetText($"Score:{GameManager.Instance().score}");
            player.SetActive(false);
            result.SetActive(true);
            score.gameObject.SetActive(false);
            time.gameObject.SetActive(false);
        }
    }

    public void ButtonClick(Button b)
    {
        //�p�G���s�O�^��D��(Back)�A�h����MenuScene
        if (b.name.Equals("Back"))
        {
            GameManager.Instance().score = 0;
            GameManager.Instance().ChangeScene("MenuScene");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
