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
    public GroundCheck ground;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
        maxjump = 2; //if文でフラグ取得の判定
        restjump = maxjump;
    }

    void Update()
    {
        restjump=ground.IsGround(maxjump, restjump);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void FixedUpdate()
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
            rb.velocity = new Vector2(0,rb.velocity.y);
        }


    }

    private void Jump()
    {
        if (restjump > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 5);
            restjump -= 1;
        }
    }
}
