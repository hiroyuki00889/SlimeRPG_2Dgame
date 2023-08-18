using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;

public class Enemy_Move : MonoBehaviour
{
    public float speed;
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
                rb.velocity = new Vector2(xVector * speed, rb.velocity.y);
            }
            else
            {
                rb.Sleep();
            }
        }
        else 
        {
            Destroy(this.gameObject);
            /*if (!dead) //éÄÇÒÇæèuä‘ÇÃèàóù
            {
                //animator.Play(dead);
                dead = true;
            }
            else 
            {
            //éÄÇÒÇæå„ÇÃó]âC
            }*/
        }
        }
}
