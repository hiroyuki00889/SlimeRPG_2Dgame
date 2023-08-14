using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private int canjump = 1; //ƒWƒƒƒ“ƒv‰ñ”
    Animator animator;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
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

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (canjump > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 5);
            canjump -= 1;
        }
    }
}
