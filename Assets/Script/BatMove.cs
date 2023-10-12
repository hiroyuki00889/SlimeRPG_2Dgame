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
    private GameObject player;
    [SerializeField] private GameObject ultraSounds;

    private Vector3 destination;

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
                StartCoroutine(Magic());
                Debug.Log("�I��");
            }
                    
        }
        else 
        {
            rb.Sleep();
            //�������Ƃ���ɖ߂鏈��
        }
    }

    private IEnumerator FirstMove()
    {
        Debug.Log("aaaaa");
        while (destination.y < this.transform.position.y)
        {
            rb.velocity = new Vector3(0, -3, 0);
            yield return null;
        }
        rb.velocity =Vector2.zero;
                move = true;
                yield break;
            //�v���C���[�̍����܂œ����I���܂ő҂�
    }

    private IEnumerator Magic() 
    {
        punish= true;
        Instantiate(ultraSounds, this.transform.position, Quaternion.identity);//�X�L���������㌄ �X�L���̎��Ԃ�蒷�߂ɑҋ@������
        yield return new WaitForSeconds(2f);
        if (player.transform.position.x < this.transform.position.x)
        {
            destination.x = -5;
        }
        else 
        {
            destination.x = 5;
        }
        if (player.transform.position.y < this.transform.position.y)
        {
            destination.y = -5;
        }
        else 
        {
            destination.y = 5;
        }
        rb.velocity = destination;
        yield return new WaitForSeconds(1f);
        rb.velocity = Vector2.zero;
        punish = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !move) 
        {
            //�ŏ��̃��[�u �v���C���[�̍����܂łȂ�
            player = collision.gameObject;
            destination = player.transform.position;
            Debug.Log(destination);
            StartCoroutine(FirstMove());
        }
    }
}
