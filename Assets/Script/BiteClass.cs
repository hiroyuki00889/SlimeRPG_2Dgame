using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteClass : MonoBehaviour
{
    private GameObject oyaOb;
    private Rigidbody2D rb;

    private void Update()
    {

    }

    public void KamuSkill(bool right, GameObject kariOb)
    {
        oyaOb = kariOb;
        rb = GetComponent<Rigidbody2D>();
        if (right)
        {
            rb.AddForce(new Vector3(10, 0, 0), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector3(-10, 0, 0), ForceMode2D.Impulse);
        }
        Invoke(("Destroy1"), 2f);
    }

    private void Destroy1()
    {
        oyaOb.gameObject.SetActive(true);
        Destroy(this.gameObject);
    }

    
}
