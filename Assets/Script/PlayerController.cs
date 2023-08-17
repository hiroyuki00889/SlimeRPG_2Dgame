using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private int maxjump;
    private int restjump; //ジャンプ回数
    Animator animator;
    private BoxCollider2D boxcollider;
    [Header("踏みつけ判定の割合")] public float stepOnRate;
    public GroundCheck ground; //接地判定用

    private bool down=false; //死亡フラグ

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
        boxcollider= GetComponent<BoxCollider2D>();
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
        }
    }

    void FixedUpdate()
    {
        if (!down)
        {
            if (Input.GetKey(KeyCode.D))
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
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Enemy")
       {
            float stepOnHeight = (boxcollider.size.y * (stepOnRate / 100f));
        //踏みつけ判定のワールド座標
        float judgePos = transform.position.y - (boxcollider.size.y / 2f) + stepOnHeight;
        foreach (ContactPoint2D p in collision.contacts)
        {
            if (p.point.y < judgePos)
            {
                    //踏んだ時の処理
                //animator.Play("change1");
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
