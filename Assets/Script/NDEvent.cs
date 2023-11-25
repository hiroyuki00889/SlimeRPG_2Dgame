using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NDEvent : MonoBehaviour
{

    [SerializeField] AnimateNDialog animateNDialog;
    bool isCallOnece = false;
    public Text ndEventText;
    int i = 0;
    public bool isNDEvent = false;
    public bool FirstEvent;//チェックありなしで表示する文章を選択

    string[] words = {"魔物の縄張りに住むスライムは平和に暮らしていました",
        "しかし、あることで縄張りを治める四天王にムカついてしまいました","スライムは四天王を倒す旅に出ます"};

    ////辞書定義
    //private Dictionary<string, string> ndEvent = new Dictionary<string, string>();

    //private void Start()
    //{
    //    ndEvent.Add("EventTagA", "イベントタグAの文章です");
    //    ndEvent.Add("EventTagB", "イベントタグBの文章です");
    //    ndEvent.Add("EventTagC", "イベントタグCの文章です");
    //    ndEvent.Add("EventTagD", "イベントタグDの文章です");
    //    // ここに会話文追加
    //}

    //public void StartNDEvent(string eventTag)
    //{
    //    // コライダーのタグで会話内容を取得
    //    if(ndEvent.TryGetValue(eventTag, out string ndEventText))
    //    {
    //        // 会話内容を表示
    //        DisplayNDEvent(ndEventText);
    //    }
    //    //else
    //    //{
    //    //    エラーハンドリングの内容をここに記述
    //    //}
    //}

    //void DisplayNDEvent(string text)
    //{
    //    // 会話内容をUIに表示する処理
    //    ndEventText.text = text;
    //    // 文章を送って表示する処理を以下に記述
    //    // 思いつきません
    //}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //一度しか呼び出されないフラグがfalseかつ、衝突してきた相手がPlayer
        if (!isCallOnece && collider.gameObject.CompareTag("Player"))
        {
            isCallOnece = true;
            
            isNDEvent = true;
            CoiceSentence();
        }
    }

    private void CoiceSentence()
    {
        
        if (FirstEvent)
        {
            ndEventText.text = words[i];
        }
        
    }

    private void Update()
    {
        //ナレーターダイアログが開いている間、左クリックを押すと、文章送り
        if (animateNDialog.IsOpen && animateNDialog.n_SentenceTrigger == true)
        {
            if (i < words.Length - 1)
            {
                i++;
                ndEventText.text = words[i];
                animateNDialog.n_SentenceTrigger = false;　//クリックを押さなくても文章送りされるのを防ぐ
            }

            //最後の文章になって、左クリックを押すとフラグオフ、Textオブジェクトを非アクティブにする
            if (i >= words.Length - 1)
            {
                isNDEvent = false;
            }
        }
    }
}
