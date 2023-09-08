using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private int maxjump;
    private int restjump; //ジャンプ回数
    Animator animator;
    private CapsuleCollider2D capsulecollider;
    [Header("踏みつけ判定の割合")] public float stepOnRate;
    public GroundCheck ground; //接地判定用

    private float time = 1;
    private bool right = false;
    private bool down=false; //死亡フラグ

    [SerializeField] private Skill_Table st;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
        capsulecollider= GetComponent<CapsuleCollider2D>();
        maxjump = 3; //if文でフラグ取得の判定
        restjump = maxjump;
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

            if (Input.GetKeyDown(KeyCode.R)) 
            {
                Instantiate(st.skill[0].skill_effect,this.transform.position,Quaternion.identity);
                //ここにst.countを減らす処理
            }
        }
    }

    void FixedUpdate()
    {
        if (!down)
        {
            Move();
            /*if (Input.GetKey(KeyCode.D))
            {
                        rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }*/
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
        if (collision.collider.name == "Enemy")
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
                //ダウンする
                //animator.Play("Player_Down"); //死んだ時のアニメーション
                down = true;
            }
          }
       }
    }
}
