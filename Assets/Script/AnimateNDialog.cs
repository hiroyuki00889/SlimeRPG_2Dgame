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
    public bool IsOpen => gameObject.activeSelf;　// ダイアログは開いているかどうか
    public bool IsTransition = false;　// アニメーション中かどうか
    public bool enterTrigger = false;　//エンターキーで会話を進めるフラグ
    public bool isNDialog = false; //NDialog表示フラグ

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //ナレーターダイアログがアクティブかつ、エンターキーでナレーターの文章を進めるトリガー設定
        if (Input.GetKeyDown(KeyCode.Return) && gameObject.activeSelf == true)
        {
            enterTrigger = true;
        }
        //衝突した相手のタグがEventsかつ、1度きりの各フラグがオンになっているか
        if (isNDialog == true && firstEvent.isFirstEvent == true
            )
        {
            DialogNarratorOpen();

        }
        //
        if (enterTrigger == true && firstEvent.isFirstEvent == false) 
        {
            DialogNarratorClose();
            enterTrigger = false;
            isNDialog= false;
        }
    }

    private void DialogNarratorOpen()
    {
        if (IsOpen || IsTransition) return; // 不正操作防止
        gameObject.SetActive(true); // DialogNarrator自体をアクティブにする
        m_Animator.SetBool(ParamIsOpen, true); // IsOpenフラグをtrueにセット
        // アニメーション待機
        StartCoroutine(WaitAnimation("Shown"));
    }

    private void DialogNarratorClose()
    {   
        if (!IsOpen || IsTransition) return;
        m_Animator.SetBool(ParamIsOpen, false); // IsOpenフラグをfalseにセット
        // アニメーション待機し、終わったらパネル自体を非アクティブにする
        StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));
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
