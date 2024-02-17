using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NDEvent : MonoBehaviour
{

    [SerializeField] AnimateNDialog animateNDialog;
    private string callOnece;
    public Text ndEventText;
    private string[] parts;
    int i = 0;
    public bool isNDEvent;
    ////辞書定義
    private Dictionary<string, string> ndEvent = new Dictionary<string, string>();

    private void Start()
    {
        //会話文追加。「,,」で文章の区切り。タグはUnityの方と合わせて、NDEventを付ける
        ndEvent.Add("NDEventTag1-A", "あっ、あれは犬の魔物です,,近づくと突撃してくるから注意して！,,無防備な上から覆いかぶされば魔物を食べることができるよ");
        ndEvent.Add("NDEventTagB", "イベントタグBの文章です");
        ndEvent.Add("NDEventTagC", "イベントタグCの文章です");
        ndEvent.Add("NDEventTagD", "イベントタグDの文章です");

        ndEvent.Add("Title1", "SLIME RPG へようこそ,,あなたはスライムとして冒険してもらいます,,そこの家のドアに行くと冒険が始まります,,Aキー、Dキーで左右に移動\nFキーを押しながら移動で早く動けます,,Spaceキーでジャンプすることが出来ます,,では、よい冒険を！");
        //Stage1初回起動時の表示文章、実行はStage1スクリプト
        ndEvent.Add("Stage1", "魔物の縄張りに住むスライムは平和に暮らしていました,,しかし、あることで縄張りを治める四天王にムカついてしまいました,,スライムは四天王を倒す旅に出ます");
        ndEvent.Add("Skill", "やったね！\n犬の魔物を飲み込めたね！,,スライムは飲み込んだ敵の情報を取り込んでスキルとして使えるようになるよ,,スキルは画面の下に表示されるよ,,使いたいスキルにカーソルを合わせてクリックしてね");
        
    }

    public void StartNDEvent(string eventTag)
    {
        // コライダーのタグで会話内容を取得
        if (ndEvent.TryGetValue(eventTag, out string ndEventText))
        {
            // 会話内容を表示
            DisplayNDEvent(ndEventText);
            animateNDialog.DialogNarratorOpen();
            callOnece = eventTag;
            Debug.Log("StartNDEvent");
        }
        else
        {
            //エラーハンドリングの内容をここに記述
            Debug.Log("ナレーターダイアログのタグか文章の辞書が上手く参照されていない");
        }
    }

    void DisplayNDEvent(string text)
    {
        string delimiter = ",,"; //区切り文字
        //区切り文字で区切ったパートを配列に入れる処理
        parts = text.Split(new[] { delimiter }, StringSplitOptions.None);
        //最初の文章入力
        ndEventText.text = parts[0];
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //衝突してきた相手がPlayerならplayercontrollerのフラグオン
        if (collider.gameObject.CompareTag("Player"))
        {
            isNDEvent = true;
        }
    }

    private void Update()
    {
        //ナレーターダイアログが開いている間、左クリックを押すと、文章送り
        if (animateNDialog.IsOpen && animateNDialog.n_SentenceTrigger == true && parts != null)
        {
            if (i < parts.Length - 1)
            {
                i++;
                ndEventText.text = parts[i];
                animateNDialog.n_SentenceTrigger = false;　//クリックを押さなくても文章送りされるのを防ぐ
            }

            //最後の文章になって、左クリックを押すとフラグオフ、Textオブジェクトを非アクティブにする
            if (i >= parts.Length - 1)
            {
                Array.Clear(parts, 0, parts.Length);
                isNDEvent = false;
                i=0;
                ndEvent.Remove(callOnece);
            }
        }
    }
}
