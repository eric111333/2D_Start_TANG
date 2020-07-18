using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Text textTime;

    private void Start()
    {
        textTime = GameObject.Find("時間").GetComponent<Text>();
    }
    private void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        textTime.text = "時間：" + Time.timeSinceLevelLoad.ToString("f1");
    }
}
