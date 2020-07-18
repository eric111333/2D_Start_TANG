using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Animator aniPlayer;
    private SpriteRenderer sprPlayer;
    private float hp = 100;
    private float hpMax;
    private Image hpBar;
    private bool dead;

    [Header("移動速度"), Range(0, 100)]
    public float speed = 10;
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
        aniPlayer.SetTrigger("hurt");
        this.enabled = false;
        dead = true;

    }

    private void Start()
    {

        sprPlayer = GetComponent<SpriteRenderer>();
        aniPlayer = GetComponent<Animator>();
        hpBar = GameObject.Find("血條").GetComponent<Image>();

        hpMax = hp;

    }

    /// <summary>
    /// 移動
    /// </summary>
 
    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dead) return;

        if (collision.tag == "陷阱")
        {
            hp -= 20;
            hpBar.fillAmount = hp / hpMax;

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
