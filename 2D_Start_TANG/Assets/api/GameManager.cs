using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Text textTime;
    private bool gameOver;

    [Header("結束畫面")]
    public GameObject final;

    private void Start()
    {
        textTime = GameObject.Find("時間").GetComponent<Text>();
    }
    private void Update()
    {
        UpdateTime();
    }
    /// <summary>
    /// 更新遊戲時間
    /// </summary>
    private void UpdateTime()
    {
        if (gameOver) return;
        textTime.text = "時間：" + Time.timeSinceLevelLoad.ToString("f2");
    }

    /// <summary>
    /// 遊戲結束
    /// </summary>
    public void GameOver()
    {
        gameOver = true;
        final.SetActive(true);

    }

    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    public void Replay()
    {
        SceneManager.LoadScene("選單");
    }
}
