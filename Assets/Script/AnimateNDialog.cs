using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimateNDialog : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private int layer;
    [SerializeField] FirstEvent firstEvent;
    //[SerializeField] FirstEnemy firstEnemy;
    // IsOpenフラグ(アニメーターコントローラー内で定義したフラグ)
    private static readonly int ParamIsOpen = Animator.StringToHash("IsOpen");
    // ダイアログは開いているかどうか
    public bool IsOpen => gameObject.activeSelf;//= false;
    // アニメーション中かどうか
    public bool IsTransition { get; private set; }
    //エンターキーで会話を進めるフラグ
    public bool enterTrigger = false;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        Debug.Log(IsOpen);
        Debug.Log(gameObject.activeSelf);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //衝突した相手のタグがEventsかつ、1度きりのフラグがオンになっているか
        if (collision.gameObject.CompareTag("Events") && firstEvent.isFirstEvent == true
            )
        {
            DialogNarratorOpen();
        }
    }
    private void Update()
    {
        //ナレーターダイアログがアクティブかつ、エンターキーでナレーターの文章を進めるトリガー設定
        if (Input.GetKeyDown(KeyCode.Return) && gameObject.activeSelf == true)
        {
            enterTrigger = true;
        }
        //
        if(enterTrigger == true && firstEvent.isFirstEvent == false) 
        {
            DialogNarratorClose();
            enterTrigger = false;
        }
    }

    private void DialogNarratorOpen()
    {
        // 不正操作防止
        if (IsOpen || IsTransition) return;
        // DialogNarrator自体をアクティブにする
        gameObject.SetActive(true);//IsOpen = true;
        // IsOpenフラグをtrueにセット
        m_Animator.SetBool(ParamIsOpen, true);
        // アニメーション待機
        StartCoroutine(WaitAnimation("Shown"));
    }

    private void DialogNarratorClose()
    {   
        if (!IsOpen || IsTransition) return;
        // IsOpenフラグをfalseにセット
        m_Animator.SetBool(ParamIsOpen, false);
        // アニメーション待機し、終わったらパネル自体を非アクティブにする
        StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));//IsOpen = false;
    }

    private IEnumerator WaitAnimation(string stateName, UnityAction onCompleted = null)
    {
        //このブール値がtrueの間は上2つの関数が動かない
        IsTransition = true;

        yield return new WaitUntil(() =>
        {
              //ステートが変化し、アニメーションが終了するまで待機
              var state = m_Animator.GetCurrentAnimatorStateInfo(layer);
              return state.IsName(stateName) && state.normalizedTime >= 1;
        });

        IsTransition = false;
        onCompleted?.Invoke();
    }
}
