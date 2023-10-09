using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMove : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    private bool move = false;
    private bool punish = false;
    [SerializeField] private GameObject ultraSounds;
    private float time = 0f;

    void Start()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (spriteRenderer.isVisible)
        {
            
            if (!punish && move)
            { 
                time += Time.fixedDeltaTime;
                Debug.Log(time);
                if (time > 2f) 
                {
                    
                    rb.velocity = Vector2.zero;
                    Magic();
                }
            }
                    
        }
        else 
        {
            rb.Sleep();
            move= false;   
        }
    }

    private void Magic() 
    {
        punish = true;
        Instantiate(ultraSounds, this.transform.position, Quaternion.identity);
        //アニメーション終わるまで待つ処理,または後隙の設定
        Debug.Log("Magic");
        Invoke("Punish", 1f);
    }

    private void Punish()
    {
        punish = false;
        //ここに次の動きの処理
        time = 0;
        rb.velocity = new Vector2(3*RandomMove(),3*RandomMove());
        Debug.Log("Punish");
    }


    private int RandomMove() 
    {
        int i = Random.Range(-1, 2);
        if (i == -1)
        {
            return -1;
        }
        else if (i == 0)
        {
            return 0;
        }
        else 
        {
            return 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !move) 
        {
            //最初のムーブ プレイヤーの高さまでなど
            move = true;
            rb.AddForce(new Vector2(0, -3), ForceMode2D.Impulse);
        }
    }
}
