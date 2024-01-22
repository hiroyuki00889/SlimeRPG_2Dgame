using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;

public class BettleMove : MonoBehaviour
{
    private int hp;
    [SerializeField]private GameObject player;
    private Rigidbody2D rb;
    private Animator anim;
    private CircleCollider2D circleCollider;
    [SerializeField]private Vector2 rightrush;//âEë§Ç©ÇÁìÀåÇèâä˙à íu
    [SerializeField] private Vector2 rightrushend;//é~Ç‹ÇÈà íu
    [SerializeField]private Vector3[] rightrushdetour;
    [SerializeField]private Vector2 leftrush;
    [SerializeField] private Vector2 leftrushend;
    [SerializeField]private Vector3[] leftrushdetour;
    [SerializeField]private float digholedown;
    [SerializeField]private float digholeup;
    private int nextmove;
    private bool movenow;
    [SerializeField] private float dighole;
    private bool digholenow;

    [SerializeField] private Vector3[] walkpath;
    
    private void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        circleCollider=GetComponent<CircleCollider2D>();
        nextmove = Random.Range(0,2);
        rightrushdetour[0] = rightrush;
        leftrushdetour[0] = leftrush;
    }
    private void FixedUpdate()
    {
        if (!movenow)
        {
            if (nextmove == 0)
            {
                StartCoroutine(DigHole());
            }
            else if (nextmove == 1)
            {
                StartCoroutine(BettleRush());
            }
            else if (nextmove == 2) 
            {
                StartCoroutine(DigHole());
            }
        }
        else 
        {
            if (digholenow) 
            {
                if (this.transform.position.x == player.transform.position.x) 
                {
                    return;
                }
                if (this.transform.position.x < player.transform.position.x)
                {
                    rb.velocity = new Vector2(7, 0);
                }
                else 
                {
                    rb.velocity = new Vector2(-7, 0);
                }
            }
            return;
        }
    }

    private IEnumerator BettleWalk()
    {
        //int rnd = Random.Range(0, walkpath.Length);
        yield return this.transform.DOPath(walkpath,3f).WaitForCompletion();
    }

    private IEnumerator BettleRush() 
    {
        movenow= true;
        int rnd=Random.Range(0,2);
        if (rnd == 0)
        {
            yield return this.transform.DOMove(rightrush, 2f).WaitForCompletion();
            yield return this.transform.DOPath(rightrushdetour,1f).WaitForCompletion();
            yield return this.transform.DOMove(new Vector2(rightrushend.x+1,rightrushend.y),1f).WaitForCompletion();
            yield return this.transform.DOMove(leftrush, 1f).WaitForCompletion();
            yield return this.transform.DOPath(leftrushdetour, 1f).WaitForCompletion();
            yield return this.transform.DOMove(leftrushend, 1f).WaitForCompletion();
        }
        else
        {
            yield return this.transform.DOMove(leftrush, 2f).WaitForCompletion();
            yield return this.transform.DOPath(leftrushdetour, 1f).WaitForCompletion();
            yield return this.transform.DOMove(new Vector2(leftrushend.x - 1, leftrushend.y), 1f).WaitForCompletion();
            yield return this.transform.DOMove(rightrush, 1f).WaitForCompletion();
            yield return this.transform.DOPath(rightrushdetour, 1f).WaitForCompletion();
            yield return this.transform.DOMove(new Vector2(rightrushend.x - 1, rightrushend.y), 1f).WaitForCompletion();
        }
        yield return StartCoroutine(Down());
        nextmove = Random.Range(0, 2);
        movenow = false;
    }

    private IEnumerator DigHole()
    {
        movenow= true;
        yield return this.transform.DOMoveY(dighole,2f).WaitForCompletion();
        rb.bodyType = RigidbodyType2D.Dynamic;
        digholenow = true;
        yield return new WaitForSeconds(2f);
        digholenow = false;
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        yield return this.transform.DOMoveY(dighole+8,1f).SetDelay(0.2f).WaitForCompletion();
        yield return StartCoroutine(Down());
        nextmove = Random.Range(0, 2);
        movenow = false;
    }

    private IEnumerator Down()
    {
        yield return this.transform.DORotate(new Vector3(180, 0, 0), 0.5f).WaitForCompletion();
        rb.bodyType= RigidbodyType2D.Dynamic;
        circleCollider.isTrigger= false;
        yield return new WaitForSeconds(2f);
        circleCollider.isTrigger = true;
        rb.bodyType= RigidbodyType2D.Kinematic;
        yield return this.transform.DORotate(Vector3.zero, 0.5f).WaitForCompletion();
        yield return StartCoroutine(BettleWalk());
    }
}
