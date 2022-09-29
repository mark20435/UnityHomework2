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

    public void ButtonClick(Button b)
    {
        if (b.name.Equals("Start"))
        {
            GameManager.Instance().ChangeScene("GameScene");
        }
    }
}
