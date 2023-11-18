using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;
using System.Runtime.CompilerServices;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.Events;

public class Enemy_Move : MonoBehaviour
{
    public float speed;
    private float time = 0;
    //public float gravity;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private bool right = false;
    // private bool dead = false;
    private ObjectCollision oc;
    public EnemyCollisionCheck check;
    private GameObject playerOb;
    private float playerpos_x;
    private float enemypos_x;
    private float distance;
    private bool coroutine = false;
    private bool enemyRight = true;

    private bool opposumright;
    private Coroutine pigcoroutine = null;

    [SerializeField] private bool Bunny, Bat, Dog, Opossum, pig;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

            } else if (spriteRenderer.isVisible && Dog && coroutine == false)
            {
                //Playerと敵の位置変数の用意
                playerpos_x = playerOb.transform.position.x;
                enemypos_x = transform.position.x;
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
            } else if (pig)
            {
                PigMove();
            }
        }
        else
        {
            //敵オブジェクトを破壊する
            Destroy(this.gameObject);
            // プレイヤーに当たった場合、敵のタグに応じたカウンターを増加
            EnemyTagCounter enemyTagCounter = FindObjectOfType<EnemyTagCounter>();
            // EnemyTags = GameObject.FindGameObjectsWithTag("Enemy");
            if (gameObject.CompareTag("Bunny"))
            {
                enemyTagCounter.IncrementCounter("Bunny");
            }
            else if (gameObject.CompareTag("Dog"))
            {
                enemyTagCounter.IncrementCounter("Dog");
            }
            else if (gameObject.CompareTag("Bat"))
            {
                enemyTagCounter.IncrementCounter("Bat");
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

        }
    }

    private void BatMove()
    {
        //蝙蝠の動き
    }

    private void DogMove()
    {
        animator.SetBool("goState", false);
        if (playerpos_x < enemypos_x)　//敵の位置がPlayerより右の場合
        {
            enemyRight = true;
            //犬は左に歩く,animation
            transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            animator.SetFloat("speed", rb.velocity.x * -1);
            //Playerとenemyの位置変数
            distance = Vector3.Distance(transform.position, playerOb.transform.position);
            //Playerとの位置が5以内でアニメーション変更
            if (distance <= 5)
            {
                StartCoroutine(DogAttack());
            }
        }
        else if (playerpos_x > enemypos_x)　//敵の位置がPlayerより左の場合
        {
            enemyRight = false;
            //犬は右に歩く,animation
            transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(speed, rb.velocity.y);
            animator.SetFloat("speed", rb.velocity.x);
            //Playerとenemyの位置変数
            distance = Vector3.Distance(transform.position, playerOb.transform.position);
            //Playerとの位置が5以内でアニメーション変更
            if (distance <= 5)
            {
                StartCoroutine(DogAttack());
            }
        }
    }

    IEnumerator DogAttack()
    {
        coroutine = true;
        animator.SetBool("attack", true);
        yield return new WaitForSeconds(1);
        //犬が右にいるので左に突撃、Animation切り替え
        if (enemyRight == true)
        {
            rb.AddForce(new Vector2(-15, 0), ForceMode2D.Impulse);
            Invoke(("Stop"), 0.4f);
            StartCoroutine(WaitAnimation("Dog_Attack"));            
            yield return new WaitForSeconds(2);           
        }
        //犬が左にいるので右に突撃、Animation切り替え
        else if (enemyRight == false)
        {
            rb.AddForce(new Vector2(15, 1), ForceMode2D.Impulse);
            Invoke(("Stop"), 0.4f);
            StartCoroutine(WaitAnimation("Dog_Attack"));
            yield return new WaitForSeconds(2);    
        }
        animator.SetTrigger("stop");
        animator.SetBool("goState", true);
        animator.SetBool("attack", false);
        
        coroutine = false;
    }

    private void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    private IEnumerator WaitAnimation(string stateName, UnityAction onCompleted = null)
    {
        yield return new WaitUntil(() =>
        {
            //ステートが変化し、アニメーションが終了するまで待機
            var state = animator.GetCurrentAnimatorStateInfo(0);
            return state.IsName(stateName) && state.normalizedTime >= 1;
        });
        onCompleted?.Invoke();
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


    private void PigMove()
    {
        if (spriteRenderer.isVisible)
        {
            int xVector = -1;
            if (playerOb.transform.position.x>this.transform.position.x)
            {
                xVector = 1;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (pigcoroutine == null)
            {
                if ((playerOb.transform.position - this.transform.position).sqrMagnitude < 25)
                {
                    pigcoroutine = StartCoroutine(PigJump(xVector));
                }
                else
                {
                    rb.velocity = new Vector2(xVector * speed, rb.velocity.y);
                }
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

    private IEnumerator PigJump(int xVector) 
    {
        rb.gravityScale = 0;
        rb.velocity = new Vector2(3*xVector, 6);
        yield return new WaitForSeconds(0.5f); //上昇
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.4f);//下降
        rb.velocity = new Vector2(0, -20);
        yield return new WaitUntil(() =>rb.velocity.y==0);
        yield return new WaitForSeconds(1f);
        rb.gravityScale = 1;
        yield return new WaitForSeconds(0.5f);
        pigcoroutine = null;
        yield break;
    }
}
