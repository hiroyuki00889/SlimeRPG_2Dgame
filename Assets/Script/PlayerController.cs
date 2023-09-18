using JetBrains.Annotations;
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

    [SerializeField] private CursorScript cursorscript;
    [SerializeField] private EnemyTagCounter enemyTagCounter;
    [SerializeField] private Skill_Table st;
    [SerializeField] private Animator m_Animator;
    [SerializeField] FirstEvent firstEvent;
    [SerializeField] AnimateNDialog animateNDialog;

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
                switch (cursorscript.GetSkillNow()) 
                {
                    case "EnemyTagA":
                        Instantiate(st.skill[0].skill_effect, this.transform.position - new Vector3(0, -1, 0), Quaternion.identity); //skill[Getnowskill]�݂����Ȋ����ɂȂ�
                        rb.AddForce(new Vector3(0, 200, 0), ForceMode2D.Impulse);
                        enemyTagCounter.enemyTagCounters["EnemyTagA"] -= 1;
                        break;
                }
                
                //;
                   //���I�����Ă�X�L���̎c��񐔈��������Ă���->����EnemyTagCounter�̒l�����炷
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
            this.transform.localScale =Vector3.one;
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
            this.transform.localScale = new Vector3(-1,1,1);
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
        //NDialog�\���̃t���Oon�F�Փ˂�������̃^�O��Events
        if (collision.gameObject.CompareTag("Events"))
        {
            animateNDialog.isNDialog = true;
            Debug.Log("playerflagOK");
        }

        if (collision.gameObject.CompareTag("EnemyTagA") || collision.gameObject.CompareTag("EnemyTagB"))
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //AnimateNDialog animateNDialog = gameObject.AddComponent<AnimateNDialog>();
        //FirstEvent firstEvent = gameObject.AddComponent<FirstEvent>();
        //�Փ˂�������̃^�O��Events���A1�x����̃t���O���I���ɂȂ��Ă��邩
        if (collision.gameObject.CompareTag("Events") && firstEvent != null && firstEvent.isFirstEvent == true
            )
        {
            animateNDialog.DialogNarratorOpen();
        }
    }
}
