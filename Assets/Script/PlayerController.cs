using JetBrains.Annotations;
using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    private EnemyTagCounter enemyTagCounter;
    private int maxjump;
    private int restjump; //ジャンプ回数
    Animator animator;
    private CapsuleCollider2D capsulecollider;
    [Header("Playerの踏みつけ判定足下から何割足すか")] public float stepOnRate;

    public GroundCheck ground; //接地判定用

    private float time = 1;
    public bool right = false;
    public bool down=false; //死亡フラグ
    private bool small;
    private float cashe_steponrate;
    //[SerializeField] private CursorScript cursorscript;
    //[SerializeField] private Skill_Activate skill_Activate;
    [SerializeField] private Animator m_Animator;
    [SerializeField] NDEvent ndEvent;
    [SerializeField] AnimateNDialog animateNDialog;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
        capsulecollider= GetComponent<CapsuleCollider2D>();
        enemyTagCounter= GetComponent<EnemyTagCounter>();
        maxjump = 3; //if文でフラグ取得の判定
        restjump = maxjump;
        cashe_steponrate = stepOnRate;
    }

    void Update()
    {
        if (!down) 
        {
            restjump = ground.IsGround(maxjump, restjump);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
    }

    void FixedUpdate()
    {
        if (!down)
        {
            Move();
        }
        else 
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Jump()
    {
        if (/*restjump > 0*/restjump>0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 5);
            restjump -= 1;
        }
    }

    private void Move()
    {
        
        if (Input.GetKey(KeyCode.D))
        {
            if (!small)
            {
                this.transform.localScale = Vector3.one;
            }
            else 
            {
                this.transform.localScale = new Vector3(1f, 0.5f, 1f);
            }
            if (!right) {
                time = 1;
                right = true; 
            }

            if (Input.GetKey(KeyCode.F))
            {
                if (rb.velocity.x < 10)
                {
                    rb.velocity = new Vector2(speed * time, rb.velocity.y);
                    time += Time.fixedDeltaTime;
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                }
            }
            else 
            {
                time = 1;
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (!small)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
            else 
            {
                this.transform.localScale = new Vector3(-1f, 0.5f, 1f);
            }

            if (right) 
            {
                time = 1;
                right = false;
            }

            if (Input.GetKey(KeyCode.F))
            {
                if (rb.velocity.x > -10)
                {
                    rb.velocity = new Vector2(-speed * time, rb.velocity.y);
                    time += Time.fixedDeltaTime;
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                }
            }
            else 
            {
                time = 1;
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
        else
        {
            time = 1;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Bunny") || collision.gameObject.CompareTag("Dog") || collision.gameObject.CompareTag("Bat"))
       {
            float stepOnHeight = (capsulecollider.size.y * (stepOnRate / 100f));
        //踏みつけ判定のワールド座標
        float judgePos = transform.position.y - (capsulecollider.size.y / 2f) + stepOnHeight;
        foreach (ContactPoint2D p in collision.contacts)
        {
            if (p.point.y < judgePos)
            {
                    //踏んだ時の処理
                    //animator.Play("change1");
                    collision.gameObject.GetComponent<ObjectCollision>().step = true;
            }
            else
            {
                    if (!small)
                    {
                        //ダウンする
                        //animator.Play("Player_Down"); //死んだときのアニメーション
                        down = true;
                    }
                    else 
                    {
                        small= false;
                        stepOnRate = cashe_steponrate; //エディターで入力した初期値へ
                        if (right)
                        {
                            this.transform.localScale = Vector3.one;
                        }
                        else 
                        {
                            this.transform.localScale =new Vector3(-1,1,1);
                        }
                        //無敵時間処理追加予定
                    }
            }
          }
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ナレーターイベントのbool値判定、必要な分追加していく
        if (ndEvent!=null && ndEvent.isNDEvent)
        {
            //Eventsタグでナレーターダイアログを開く
            if (collision.gameObject.CompareTag("Events") )
            {
                Debug.Log("open");
                animateNDialog.DialogNarratorOpen();
            }
        }
    }

    public void SmallSlime() 
    {
        stepOnRate = 40;
        if (right)
        {
            transform.localScale = new Vector3(1f, 0.5f, 1f);
        }
        else 
        {
            transform.localScale = new Vector3(-1f, 0.5f, 1f);
        }
        small = true;
    }
}
