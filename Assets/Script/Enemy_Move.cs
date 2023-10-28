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
    private float playerpos_x;
    private float enemypos_x;
    private bool enemyRight = true;

    private bool opposumright;

    [SerializeField] private bool Bunny, Bat, Dog, Opossum;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        oc = GetComponent<ObjectCollision>();
        playerOb = GameObject.FindWithTag("Player");
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
                        opposumright = false;
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
            if (gameObject.CompareTag("EnemyTagA"))
            {
                enemyTagCounter.IncrementCounter("EnemyTagA");
            }
            else if (gameObject.CompareTag("EnemyTagB"))
            {
                enemyTagCounter.IncrementCounter("EnemyTagB");
            }
            Debug.Log(enemyTagCounter);
        }
        if (spriteRenderer.isVisible && Dog)
        {

            //Playerと敵の位置変数の用意
            playerpos_x = playerOb.transform.position.x;
            enemypos_x = transform.position.x;
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
        //画面内に入ったら
        /*if (spriteRenderer.isVisible)
        {
            //Playerと敵の位置変数の用意
            playerpos = playerOb.transform.position;
            enemypos = transform.position;
        }*/
        if (playerpos_x < enemypos_x)　//敵の位置がPlayerより右の場合
        {
            enemyRight = true;
            //犬は左に歩く,animation
            transform.position += new Vector3(-speed * 0.01f, 0, 0);
            //Playerとの位置が5以内でアニメーション変更
            if (enemypos_x - playerpos_x <= 5 && time >= 3)
            {
                StartCoroutine(DogAttack());
                time = 0;
            }
        }
        else if (playerpos_x > enemypos_x)　//敵の位置がPlayerより左の場合
        {
            enemyRight = false;
            time += Time.fixedDeltaTime;
            //犬は右に歩く,animation
            transform.position += new Vector3(speed * 0.01f, 0, 0);
            //Playerとの位置が5以内でアニメーション変更
            if (enemypos_x - playerpos_x <= 5 && time >= 3)
            {
                StartCoroutine(DogAttack());
                time = 0;
            }
        }
        else
        {
            rb.Sleep();
        }
    }

    IEnumerator DogAttack()
    {
        yield return new WaitForSeconds(1);
        //犬が右にいるので左に突撃、Animation切り替え
        if (enemyRight == true)
        {
            Debug.Log("totugeki");
            rb.AddForce(new Vector2(-10f, 0));
            Invoke(("Stop"), 0.1f);
            yield return new WaitForSeconds(1);
        }
        //犬が左にいるので右に突撃、Animation切り替え
        else if (enemyRight == false)
        {
            Debug.Log("totugeki2");
            rb.AddForce(new Vector2(10f, 1));
        }
    }

    private void Stop()
    {
        rb.velocity = Vector2.zero;
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
