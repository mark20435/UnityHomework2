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
    /// �B�z���UButton���ƥ�
    /// </summary>
    /// <param name="b">Button����</param>
    public void ButtonClick(Button b)
    {
        //�p�G���s�O�}�l�C��(Start)�A�h����GameScene
        if (b.name.Equals("Start"))
        {
            GameManager.Instance().ChangeScene("GameScene");
        }
    }
}
