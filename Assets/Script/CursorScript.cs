using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
    public EnemyTagCounter counter;
    public float minpos;
    public float maxpos;
    public float grid;
    private BoxCollider2D boxCollider2D;
    private Text text;
    //‚Ü‚¾‰½”Ô–Ú‚É‰½‚ÌƒXƒLƒ‹‚ª‚ ‚é‚Ì‚©•ª‚©‚ç‚È‚¢ó‘Ô ‘‚«‚©‚¯

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>(); 
    }

    void Update()
    {
        maxpos=counter.GetAllCounters().Count * grid - minpos;
        if (Input.GetKeyDown(KeyCode.LeftArrow) && this.transform.position.x > minpos)
        {
            this.transform.position -= new Vector3(grid,0,0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && this.transform.position.x < maxpos) 
        {
            this.transform.position += new Vector3(grid, 0, 0);
        }

    }

    public string GetSkillNow()
    {
        if (text.text.ToString().Contains("EnemyTagA"))
        {
            Debug.Log("aaaa");
            return "EnemyTagA";
        }
        else if (text.text.ToString().Contains("EnemyTagB"))
        {
            return "EnemyTagB";
        }
        else 
        {
            return "";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        text=collision.gameObject.GetComponent<Text>();
    }
}
