using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;
using System.Runtime.CompilerServices;

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
    private GameObject playerOb;
    private Vector2 playerpos;
    private Vector2 enemypos;

    private bool opposumright;

    [SerializeField] private bool Bunny, Bat, Dog, Opossum;


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
            
            if (check.isOn)
            {
                right = !right;
            }


            if (Bunny)
            {
                BunnyMove();
            }
            else if (Bat)
            {
                BatMove();

            } else if (Dog)
            {
                DogMove();

            } else if (Opossum)
            {
                OpossumMove();
                if (time < 0.5)
                {
                    time += Time.fixedDeltaTime;
                }
                else
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        opposumright = true;
                    }
                    else 
                    {
                        opposumright= false;
                    }
                    time = 0;
                }
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
    private void BunnyMove() 
    {
        time += Time.fixedDeltaTime;
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
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (time > 3)
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

    private void BatMove() 
    {
        //蝙蝠の動き
    }

    private void DogMove() 
    {
        playerOb = GameObject.FindWithTag("Player");
        if(spriteRenderer.isVisible)
        {
            playerpos = playerOb.transform.position;
            enemypos = transform.position;
            if (playerpos.x < enemypos.x)
            {
                
            }
            else
            {
                
            }
        }
        else
        {
            rb.Sleep();
        }


    }

    private void OpossumMove() 
    {
        if (spriteRenderer.isVisible)
        {
                int xVector = -1;
                if (opposumright)
                {
                    xVector = 1;
                    transform.localScale = new Vector3(-0.5f, 0.5f, 1);
                }
                else
                {
                    transform.localScale = new Vector3(0.5f, 0.5f, 1);
                }
                rb.velocity = new Vector2(xVector * speed, rb.velocity.y);
            }
        else 
        {
            rb.Sleep();
        }
    }
}
