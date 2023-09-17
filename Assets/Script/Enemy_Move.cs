using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;
using System;

public class Enemy_Move : MonoBehaviour
{
    public float speed;
    private float time = 0;
    //public float gravity;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool right = false;
   // private bool dead = false;
    private ObjectCollision oc;
    public EnemyCollisionCheck check;


    private void Start()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
        rb= GetComponent<Rigidbody2D>();
        oc = GetComponent<ObjectCollision>();
    }



    private void FixedUpdate()
    {
        if (!oc.step)
        {
            time += Time.fixedDeltaTime;
            if (check.isOn)
            {
                right = !right;
            }
            if (spriteRenderer.isVisible)
            {
                int xVector = -1;
                if (right)
                {
                    xVector = 1;
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1,1,1);
                }
                if (time>3)
                {
                    time = 0;
                    rb.velocity = new Vector2(xVector * speed, 10);
                }
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            }
            else
            {
                rb.Sleep();
            }
        }
        else
        {
            //敵オブジェクトを破壊する
            Destroy(this.gameObject);
            // プレイヤーに当たった場合、敵のタグに応じたカウンターを増加
            EnemyTagCounter enemyTagCounter = FindObjectOfType<EnemyTagCounter>();
            // EnemyTags = GameObject.FindGameObjectsWithTag("Enemy");
            if(gameObject.CompareTag("EnemyTagA"))
            {
                enemyTagCounter.IncrementCounter("EnemyTagA");
            }
            else if (gameObject.CompareTag("EnemyTagB"))
            {
                enemyTagCounter.IncrementCounter("EnemyTagB");
            }
            Debug.Log(enemyTagCounter);

        }
        }
    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // プレイヤーに当たった場合、敵のタグに応じたカウンターを増加
            EnemyTagCounter enemyTagCounter = FindObjectOfType<EnemyTagCounter>();
            enemyTagCounter.IncrementCounter("EnemyTagA");

            // その後、敵オブジェクトを破壊するなどの処理を行う
            Destroy(this.gameObject);
        }
    }*/
}
