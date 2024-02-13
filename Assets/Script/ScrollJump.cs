using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollJump : MonoBehaviour
{
    //[SerializeField] GameObject playergo;
    [SerializeField] PlayerController pc;
    private bool isTauch;
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
            Invoke("IsTauch", 2);
            Invoke("PlayerJump",0.5f);
        }
    }
    void PlayerJump()
    {
        pc.rb.AddForce(new Vector2(1, 50), ForceMode2D.Impulse);
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
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector3(1, 0.5f, 1);
        yield return new WaitForSeconds(0.3f);
        pc.enabled = true;
        transform.localScale = new Vector3(1, 0.8f, 1);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector3(1, 1, 1);
    }
}
