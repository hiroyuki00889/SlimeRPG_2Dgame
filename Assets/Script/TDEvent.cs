using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TDEvent : MonoBehaviour
{
    [SerializeField] AnimateTDialog animateTDialog;
    bool isCallOnece = false;
    public Text nameText;
    public Text tdEventText;
    string[] parts;
    int i = 1;
    public bool isTDEvent = false;
    ////辞書定義
    private Dictionary<string, string> ndEvent = new Dictionary<string, string>();

    private void Start()
    {
        //会話文追加。「,,」で名前と文章の区切り。最初は名前。Unityの方のタグと合わせる
        ndEvent.Add("TDEventTag1-A", "悟空,,オッス、オラ悟空！,,この世界にはつえーやつがたくさんいるんだな,,オラワクワクすっぞ！");
        ndEvent.Add("TDEventTagB", "イベントタグBの文章です");
        ndEvent.Add("TDEventTagC", "イベントタグCの文章です");
        ndEvent.Add("TDEventTagD", "イベントタグDの文章です");
        // ここに会話文追加
    }

    public void StartTDEvent(string eventTag)
    {
        // コライダーのタグで会話内容を取得
        if (ndEvent.TryGetValue(eventTag, out string tdEventText))
        {
            // 会話内容を表示
            DisplayTDEvent(tdEventText);
        }
        else
        {
            //エラーハンドリングの内容をここに記述
            Debug.Log("トークダイアログのタグか文章の辞書が上手く参照されていない");
        }
    }

    void DisplayTDEvent(string text)
    {
        string delimiter = ",,"; //区切り文字
        //区切り文字で区切ったパートを配列に入れる処理
        parts = text.Split(new[] { delimiter }, StringSplitOptions.None);
        //名前入力
        nameText.text = parts[0];
        //最初の文章入力
        tdEventText.text = parts[1];
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //一度しか呼び出されないフラグがfalseかつ、衝突してきた相手がPlayer
        if (!isCallOnece && collider.gameObject.CompareTag("Player"))
        {
            isCallOnece = true;
            isTDEvent = true;
        }
    }

    private void Update()
    {
        //ナレーターダイアログが開いている間、左クリックを押すと、文章送り
        if (animateTDialog.t_IsOpen && animateTDialog.t_SentenceTrigger == true)
        {
            if (i < parts.Length - 1)
            {
                i++;
                tdEventText.text = parts[i];
                animateTDialog.t_SentenceTrigger = false;　//クリックを押さなくても文章送りされるのを防ぐ
            }

            //最後の文章になって、左クリックを押すとフラグオフ、Textオブジェクトを非アクティブにする
            if (i >= parts.Length - 1)
            {
                isTDEvent = false;
                i = 0;
            }
        }
    }
}
