using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 處理按下Button的事件
    /// </summary>
    /// <param name="b">Button物件</param>
    public void ButtonClick(Button b)
    {
        //如果按鈕是開始遊戲(Start)，則跳到GameScene
        if (b.name.Equals("Start"))
        {
            GameManager.Instance().ChangeScene("GameScene");
        }
    }
}
