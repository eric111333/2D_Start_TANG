using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;
using System.Threading;

public class createprop : MonoBehaviour
{
    [Header("要生成的道具")]
    public GameObject prop;
    [Header("x 軸最小值")]
    public float xMin;
    [Header("x 軸最大值")]
    public float xMax;
    [Header("生成頻率"), Range(0.1f, 3f)]
    public float interval = 1;

    /// <summary>
    /// 建立物件
    /// </summary>
    private void createpropObject()
    {
        float x =Random.Range(xMax, xMin);
        Vector3 pos = new Vector3(x, 8, 0);
        

        Instantiate(prop, pos, Quaternion.identity);
    }
    
   private void Start()
    {
        float r = Random.Range(0f, 2f);
        
        InvokeRepeating("createpropObject", r, interval);
    }
}
