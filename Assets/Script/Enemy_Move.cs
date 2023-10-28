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
    private bool right = false;
    // private bool dead = false;
    private ObjectCollision oc;
    public EnemyCollisionCheck check;
    private GameObject playerOb;
    private float playerpos_x;
    private float enemypos_x;
    private bool enemyRight = true;

    private bool opposumright;

    [SerializeField] private bool Bunny, Bat, Dog, Opossum;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
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

            } else if (Dog)
            {
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
            }
        }
        else
        {
            //�G�I�u�W�F�N�g��j�󂷂�
            Destroy(this.gameObject);
            // �v���C���[�ɓ��������ꍇ�A�G�̃^�O�ɉ������J�E���^�[�𑝉�
            EnemyTagCounter enemyTagCounter = FindObjectOfType<EnemyTagCounter>();
            // EnemyTags = GameObject.FindGameObjectsWithTag("Enemy");
            if (gameObject.CompareTag("EnemyTagA"))
            {
                enemyTagCounter.IncrementCounter("EnemyTagA");
            }
            else if (gameObject.CompareTag("EnemyTagB"))
            {
                enemyTagCounter.IncrementCounter("EnemyTagB");
            }
            Debug.Log(enemyTagCounter);
        }
        if (spriteRenderer.isVisible && Dog)
        {

            //Player�ƓG�̈ʒu�ϐ��̗p��
            playerpos_x = playerOb.transform.position.x;
            enemypos_x = transform.position.x;
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
            rb.Sleep();
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
            transform.position += new Vector3(-speed * 0.01f, 0, 0);
            //Player�Ƃ̈ʒu��5�ȓ��ŃA�j���[�V�����ύX
            if (enemypos_x - playerpos_x <= 5 && time >= 3)
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
            transform.position += new Vector3(speed * 0.01f, 0, 0);
            //Player�Ƃ̈ʒu��5�ȓ��ŃA�j���[�V�����ύX
            if (enemypos_x - playerpos_x <= 5 && time >= 3)
            {
                StartCoroutine(DogAttack());
                time = 0;
            }
        }
        else
        {
            rb.Sleep();
        }
    }

    IEnumerator DogAttack()
    {
        yield return new WaitForSeconds(1);
        //�����E�ɂ���̂ō��ɓˌ��AAnimation�؂�ւ�
        if (enemyRight == true)
        {
            Debug.Log("totugeki");
            rb.AddForce(new Vector2(-10f, 0));
            Invoke(("Stop"), 0.1f);
            yield return new WaitForSeconds(1);
        }
        //�������ɂ���̂ŉE�ɓˌ��AAnimation�؂�ւ�
        else if (enemyRight == false)
        {
            Debug.Log("totugeki2");
            rb.AddForce(new Vector2(10f, 1));
        }
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
}
