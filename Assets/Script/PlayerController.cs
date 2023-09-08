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
    private int restjump; //�W�����v��
    Animator animator;
    private CapsuleCollider2D capsulecollider;
    [Header("���݂�����̊���")] public float stepOnRate;
    public GroundCheck ground; //�ڒn����p

    private float time = 1;
    private bool right = false;
    private bool down=false; //���S�t���O

    [SerializeField] private Skill_Table st;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
        capsulecollider= GetComponent<CapsuleCollider2D>();
        maxjump = 3; //if���Ńt���O�擾�̔���
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
                //������st.count�����炷����
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
        //���݂�����̃��[���h���W
        float judgePos = transform.position.y - (capsulecollider.size.y / 2f) + stepOnHeight;
        foreach (ContactPoint2D p in collision.contacts)
        {
            if (p.point.y < judgePos)
            {
                    //���񂾎��̏���
                    //animator.Play("change1");
                    collision.gameObject.GetComponent<ObjectCollision>().step = true;
            }
            else
            {
                //�_�E������
                //animator.Play("Player_Down"); //���񂾎��̃A�j���[�V����
                down = true;
            }
          }
       }
    }
}
