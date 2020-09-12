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
    [Header("垂直向上推力")]
    public float yForce;
    public static bool front;
    public bool grounded, isjump;
    public Joystick joy;
    int jumpCount;


    private void FixedUpdate()
    {
        if (dead) return;
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.05f, groundLayer);
        GroundMove();
        Jump();
        SwitchAnim();
    }

    void GroundMove()
    {
        float horizontalMove = joy.Horizontal;//Input.GetAxisRaw("Horizontal");
        playerRigidbody2D.velocity =
           new Vector2(horizontalMove * speed, playerRigidbody2D.velocity.y);
        if (horizontalMove >= 0.01f)
        {
            transform.localScale = new Vector3(8 , 8, 8);
        }
        if (horizontalMove <= -0.01f)
        {
            transform.localScale = new Vector3(-8 , 8, 8);
        }
        if (horizontalMove > 0) front = true;
        if (horizontalMove < 0) front = false;
    }
    void Jump()
    {
        if (grounded)
        {
            jumpCount = 2;
            isjump = false;
        }
        if (joy.Vertical > 0.3f && grounded)
        {
            isjump = true;
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, yForce);
            jumpCount--;
            //jumpPressed = false;
        }
        else if (joy.Vertical > 0.9f && jumpCount > 0 && isjump)
        {
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, yForce);
            jumpCount--;
            //jumpPressed = false;
        }
    }
    void SwitchAnim()
    {
        aniPlayer.SetFloat("speed", Mathf.Abs(playerRigidbody2D.velocity.x));
        if (grounded)
        {
            // aniPlayer.SetBool("fall", false);
        }
        else if (!grounded && playerRigidbody2D.velocity.y > 0)
        {
            aniPlayer.SetBool("jumping", true);
        }
        else if (playerRigidbody2D.velocity.y < 0)
        {
            aniPlayer.SetBool("jumping", false);
            //aniPlayer.SetBool("fall", true);
        }
    }

    /*
    public bool JumpKey



    {
        get
        {
            return Input.GetKeyDown(KeyCode.Space);

        }
    }

    void TryJump()
    {

        aniPlayer.SetBool("jumping", JumpKey);


        if (IsGround && JumpKey)
        {

            playerRigidbody2D.AddForce(Vector2.up * yForce);
            


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

    ///    //AD左右鍵翻轉
    ///    if (Input.GetKeyDown(KeyCode.A))
    ///    {
    //        sprPlayer.flipX = true;
    //    }
    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        sprPlayer.flipX = false;
     //   }
     //   if (Input.GetKeyDown(KeyCode.LeftArrow))
    //    {
     //       sprPlayer.flipX = true;
    //    }
    //    if (Input.GetKeyDown(KeyCode.RightArrow))
    //    {
    //        sprPlayer.flipX = false;
    //    }
    }
    */

    private void Dead()
    {
        aniPlayer.SetTrigger("dead");
        this.enabled = false;
        dead = true;
        gm.GameOver();

    }

    [Header("感應地板的距離")]
    [Range(0, 1f)]
    public float distance;

    [Header("偵測地板的射線起點")]
    public Transform groundCheck;

    [Header("地面圖層")]
    public LayerMask groundLayer;

    //public bool grounded;
    /*
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
    */
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


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (dead) return;

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
