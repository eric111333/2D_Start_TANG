﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
    /// <summary>
    /// 開始遊戲
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("遊戲場景");
    }
}
