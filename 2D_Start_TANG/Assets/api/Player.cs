using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour
{
    private Animator aniPlayer;
    private SpriteRenderer sprPlayer;
    private float hp = 100;
    private float hpMax;
    private Image hpBar;
    private bool dead;
    private GameManager gm;
    Rigidbody2D playerRigidbody2D;

    [Header("移動速度"), Range(0, 100)]
    public float speed = 10;
    /// <summary>
    /// 跳躍
    /// </summary>
    [Header("垂直向上推力")]
    public float yForce;
    public bool JumpKey



    {
        get
        {
            return Input.GetKeyDown(KeyCode.Space);

        }
    }

    void TryJump()
    {

        if(IsGround && JumpKey)
        {
            
            playerRigidbody2D.AddForce(Vector2.up * yForce);
            aniPlayer.SetTrigger("jump");


        }
    }
    /// <summary>
    /// 腳色移動 動畫 翻面
    /// </summary>
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");

        transform.Translate(speed * h * Time.deltaTime, 0, 0);

        aniPlayer.SetBool("run", h != 0);
        
//AD左右鍵翻轉
        if (Input.GetKeyDown(KeyCode.A))
        {
            sprPlayer.flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            sprPlayer.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            sprPlayer.flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            sprPlayer.flipX = false;
        }
    }

    private void Dead()
    {
        aniPlayer.SetTrigger("dead");
        this.enabled = false;
        dead = true;
        gm.GameOver();

    }

    [Header("感應地板的距離")]
    [Range(0,1f)]
    public float distance;

    [Header("偵測地板的射線起點")]
    public Transform groundCheck;

    [Header("地面圖層")]
    public LayerMask groundLayer;

    public bool grounded;

    bool IsGround
    {
        get
        {
            Vector2 start = groundCheck.position;
            Vector2 end = new Vector2(start.x, start.y - distance);
            
            Debug.DrawLine(start, end, Color.blue);
            grounded = Physics2D.Linecast(start, end, groundLayer);
            return grounded;
        }
    }

    private void Start()
    {

        playerRigidbody2D = GetComponent<Rigidbody2D>(); 
        sprPlayer = GetComponent<SpriteRenderer>();
        aniPlayer = GetComponent<Animator>();
        hpBar = GameObject.Find("血條").GetComponent<Image>();
        gm = FindObjectOfType<GameManager>();

        hpMax = hp;

    }

    /// <summary>
    /// 移動
    /// </summary>
 
    private void Update()
    {
        Move();
        TryJump();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dead) return;

        if (collision.tag == "陷阱")
        {
            hp -= 20;
            hpBar.fillAmount = hp / hpMax;
            aniPlayer.SetTrigger("hurt");

            if (hp <= 0) Dead();
        }
        if (collision.tag == "櫻桃")
        {
            hp += 5;
            hp = Mathf.Clamp(hp, 0, hpMax);
            hpBar.fillAmount = hp / hpMax;
        }
        Destroy(collision.gameObject);
    }

}
