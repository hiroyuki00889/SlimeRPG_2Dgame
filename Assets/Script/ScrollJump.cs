using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollJump : MonoBehaviour
{
    //[SerializeField] GameObject playergo;
    [SerializeField] PlayerController pc;
    private bool isTauch;
    public bool up;
    public bool right;
    public bool left;
    public bool down;
    void Start()
    {
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player") && isTauch == false )
        {
            isTauch = true;
            StartCoroutine(ScrollMove());
            Invoke("IsTauch",1); //1�b�ԃW�����v���A���ŋN���Ȃ�����
            PlayerPush();   
        }
    }
    void PlayerPush()
    {
        if(up == true)
        {
            pc.rb.AddForce(new Vector2(1, 50), ForceMode2D.Impulse);
            Debug.Log("�����");
        }
        if(right)
        {
            pc.rb.AddForce(Vector2.right * 50,ForceMode2D.Impulse);
            Debug.Log("�E����");
        }
        if (left)
        {
            pc.rb.AddForce(Vector2.left * 50, ForceMode2D.Impulse);
            Debug.Log("������");
        }
        if (down == true)
        {
            pc.rb.AddForce(Vector2.down * 50, ForceMode2D.Impulse);
            Debug.Log("������");
        }
    }
    void IsTauch()
    {
        isTauch = false;
    }
    IEnumerator ScrollMove()
    {
        pc.enabled = false;
        pc.rb.velocity = Vector2.zero;
        transform.localScale = new Vector3(1, 0.8f, 1);
        yield return new WaitForSeconds(0.05f);
        transform.localScale = new Vector3(1, 0.6f, 1);
        yield return new WaitForSeconds(0.05f);
        transform.localScale = new Vector3(1, 0.3f, 1);
        yield return new WaitForSeconds(0.1f);
        pc.enabled = true;
        transform.localScale = new Vector3(1, 0.8f, 1);
        yield return new WaitForSeconds(0.05f);
        transform.localScale = new Vector3(1, 1, 1);
    }
}
