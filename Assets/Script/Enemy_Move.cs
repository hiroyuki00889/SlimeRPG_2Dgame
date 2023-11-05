using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;
using System.Runtime.CompilerServices;

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
                //Player�ƓG�̈ʒu�ϐ��̗p��
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
            //�G�I�u�W�F�N�g��j�󂷂�
            Destroy(this.gameObject);
            // �v���C���[�ɓ��������ꍇ�A�G�̃^�O�ɉ������J�E���^�[�𑝉�
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
            // �v���C���[�ɓ��������ꍇ�A�G�̃^�O�ɉ������J�E���^�[�𑝉�
            EnemyTagCounter enemyTagCounter = FindObjectOfType<EnemyTagCounter>();
            enemyTagCounter.IncrementCounter("EnemyTagA");

            // ���̌�A�G�I�u�W�F�N�g��j�󂷂�Ȃǂ̏������s��
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
        //�啂̓���
    }

    private void DogMove()
    {
        //��ʓ��ɓ�������
        /*if (spriteRenderer.isVisible)
        {
            //Player�ƓG�̈ʒu�ϐ��̗p��
            playerpos = playerOb.transform.position;
            enemypos = transform.position;
        }*/
        if (playerpos_x < enemypos_x)�@//�G�̈ʒu��Player���E�̏ꍇ
        {
            enemyRight = true;
            //���͍��ɕ���,animation
            transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            animator.SetFloat("speed", rb.velocity.x * -1);
            //Player��enemy�̈ʒu�ϐ�
            distance = Vector3.Distance(transform.position, playerOb.transform.position);
            //Player�Ƃ̈ʒu��5�ȓ��ŃA�j���[�V�����ύX
            if (distance <= 5)
            {
                StartCoroutine(DogAttack());
                time = 0;
            }
        }
        else if (playerpos_x > enemypos_x)�@//�G�̈ʒu��Player��荶�̏ꍇ
        {
            enemyRight = false;
            time += Time.fixedDeltaTime;
            //���͉E�ɕ���,animation
            transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(speed, rb.velocity.y);
            animator.SetFloat("speed", rb.velocity.x);
            //Player��enemy�̈ʒu�ϐ�
            distance = Vector3.Distance(transform.position, playerOb.transform.position);
            //Player�Ƃ̈ʒu��5�ȓ��ŃA�j���[�V�����ύX
            if (distance <= 5)
            {
                StartCoroutine(DogAttack());
                time = 0;
            }
        }
    }

    IEnumerator DogAttack()
    {
        coroutine = true;
        animator.SetBool("attack", true);
        yield return new WaitForSeconds(1);
        //�����E�ɂ���̂ō��ɓˌ��AAnimation�؂�ւ�
        if (enemyRight == true)
        {
            rb.AddForce(new Vector2(-15, 0), ForceMode2D.Impulse);
            Invoke(("Stop"), 0.4f);
            animator.SetBool("stop", true);
            yield return new WaitForSeconds(2);
        }
        //�������ɂ���̂ŉE�ɓˌ��AAnimation�؂�ւ�
        else if (enemyRight == false)
        {
            rb.AddForce(new Vector2(15, 1), ForceMode2D.Impulse);
            Invoke(("Stop"), 0.4f);
            animator.SetBool("stop", true);
            yield return new WaitForSeconds(2);
        }
        coroutine = false;
        animator.SetBool("attack", false);
        animator.SetBool("stop", false);
    }

    private void Stop()
    {
        rb.velocity = Vector2.zero;
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
                if ((playerOb.transform.position - this.transform.position).sqrMagnitude < 9)
                {
                    pigcoroutine = StartCoroutine(PigJump());
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

    private IEnumerator PigJump() 
    {
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0, 6);
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        rb.velocity = new Vector2(0, -20);
        yield return new WaitUntil(() =>rb.velocity.y==0);
        yield return new WaitForSeconds(1f);
        rb.gravityScale = 1;
        pigcoroutine = null;
        yield break;
    }
}
