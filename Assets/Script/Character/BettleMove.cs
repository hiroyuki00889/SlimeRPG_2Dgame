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
    
    private void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        nextmove = Random.Range(0,3);
        rightrushdetour[0] = rightrush;
        leftrushdetour[0] = leftrush;
    }
    private void FixedUpdate()
    {
        if (!movenow)
        {
            if (nextmove == 0)
            {
                StartCoroutine(BettleRush());
            }
            else if (nextmove == 1)
            {
                StartCoroutine(BettleRush());
            }
            else if (nextmove == 2) 
            {
                StartCoroutine(BettleRush());
            }
        }
        else 
        {
            return;
        }
    }

    private IEnumerator BettleWalk()
    {
        yield break;
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


        movenow= false;
    }

    private IEnumerator DigHole() 
    {
        yield break;
    }
}
