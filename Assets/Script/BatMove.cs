using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMove : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    private bool move = false;
    private bool punish = false;
    [SerializeField] private GameObject ultraSounds;
    private float time = 0f;

    void Start()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (spriteRenderer.isVisible)
        {
            
            if (!punish && move)
            { 
                time += Time.fixedDeltaTime;
                Debug.Log(time);
                if (time > 2f) 
                {
                    
                    rb.velocity = Vector2.zero;
                    Magic();
                }
            }
                    
        }
        else 
        {
            rb.Sleep();
        }
    }

    private void Magic() 
    {
        punish = true;
        Instantiate(ultraSounds, this.transform.position, Quaternion.identity);
        //�A�j���[�V�����I���܂ő҂���,�܂��͌㌄�̐ݒ�
        Debug.Log("Magic");
        Invoke("Punish", 2f);
    }

    private void Punish()
    {
        punish = false;
        //�����Ɏ��̓����̏���
        time = 0;
        rb.velocity = new Vector2(Random.Range(-3,4),Random.Range(-3,4));
        Debug.Log("Punish");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            //�ŏ��̃��[�u �v���C���[�̍����܂łȂ�
            move = true;
            rb.AddForce(new Vector2(0, -3), ForceMode2D.Impulse);
            Debug.Log("first"); 
        }
    }
}
