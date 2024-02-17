using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TDEvent : MonoBehaviour
{
    [SerializeField] AnimateTDialog animateTDialog;
    [SerializeField] CameraShake camShake;
    float c_seconds;
    private string callOnece;
    public Text nameText;
    public Text tdEventText;
    string[] parts;
    int i = 1;
    public bool isTDEvent;
    public bool isTalkStop;
    public bool isCamShake;
    ////辞書定義
    private Dictionary<string, string> tdEvent = new Dictionary<string, string>();

    private void Start()
    {
        //カメラが揺れる秒数
        c_seconds = camShake.shakeDuration;
        //会話文追加。「,,」で名前と文章の区切り。最初は名前。Unityの方のタグと合わせる
        //連続で会話イベント等する場合は区切り文字で余分にpart.Lengthを増やしておくこと
        tdEvent.Add("TDEventTag1-A", "ライバル,,よう！,,お前しってるか？,,四天王がここらを支配して1年、やつら天狗になってやがる,,いい加減野放しにできないよな,,なに？お前が倒すだって？,,ばかいえ、俺が倒すんだよ！,,お前より俺の必殺技のが…,, ");
        tdEvent.Add("TDEventTag1-B", "ライバル,,イベントタグBの文章です,,１行だとエラーになるのか,,aaaaaaa,,iiiiii");
        tdEvent.Add("TDEventTagC", "イベントタグCの文章です");
        tdEvent.Add("TDEventTagD", "イベントタグDの文章です");

        // 表示方法が変わっている文章
        tdEvent.Add("Guooo!!!", "???,,グゥオオオオオオォォォォォォ！！！！！,, ");
        tdEvent.Add("Bibiru", "ライバル,,ヒェッ…,,.........,,..................,,ふん、どうやら四天王の1人がご立腹のようだぜ,,ここらを牛耳っているのはクマのやつだな,,いい加減、取り締め料として食料を取られ続ける訳にはいかねぇ!,,お前、ちょっと様子見て来いよ,,この先にクマの根城があるそうだ,,俺？俺は何かあった時に動けるように陰から見てるからよ");

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //衝突してきた相手がPlayerならplayercontrollerのフラグオン
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("TDEventのOnTrigetr");
            isTDEvent = true;
        }
    }

    public void StartTDEvent(string eventTag)
    {
        // コライダーのタグで会話内容を取得
        if (tdEvent.TryGetValue(eventTag, out string tdEventText))
        {
            // 会話内容を表示
            DisplayTDEvent(tdEventText);
            animateTDialog.TDialogOpen();
            callOnece = eventTag;
        }
        else
        {
            //エラーハンドリングの内容をここに記述
            Debug.Log("トークダイアログのタグか文章の辞書が上手く参照されていないか同じ文章を表示させない処理をしたか");
        }
        /*if(eventTag == "TDEventTag1-A")
        {
            StartTDEvent("");
        }*/
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
        //foreach (string part in parts)
        //{
        //    Debug.Log(part);
        //}
        //Debug.Log(parts.Length);
    }
    

    private void Update()
    {
        //ナレーターダイアログが開いている間、左クリックを押すと、文章送り
        if (animateTDialog.t_IsOpen && animateTDialog.t_SentenceTrigger == true && parts !=null && isTalkStop == false)
        {
            if (i < parts.Length - 1)
            {
                i++;
                tdEventText.text = parts[i];
                animateTDialog.t_SentenceTrigger = false;　//クリックを押さなくても文章送りされるのを防ぐ
            }
            //差し込みイベント
            if(callOnece == "TDEventTag1-A" && tdEventText.text == " ")
            {
                TalkChange(callOnece);
            }
            //最後の文章になって、左クリックを押すとフラグオフ、Textオブジェクトを非アクティブにする
            if (i >= parts.Length - 1)
            {
                Array.Clear(parts, 0, parts.Length);
                //Debug.Log("最後の行");
                isTDEvent = false;
                i = 0;
                tdEvent.Remove(callOnece);
            }
        }
    }

    //話の最中に他の人物が話をするためのもの（俺：…、相手：…　等）
    private void TalkChange(string key)
    {
        i = 0;
        if (key == "TDEventTag1-A")
        {
            isTalkStop = true;
            StartTDEvent("Guooo!!!");
            isCamShake = true;
            Time.timeScale = 1;
            StartCoroutine(AfterShakeTalk(c_seconds,"Bibiru"));
        }
    }

    IEnumerator AfterShakeTalk(float seconds, string key)
    {
        Debug.Log("AfterShakeTalk");
        yield return new WaitForSeconds(seconds);
        isTalkStop = false;
        Time.timeScale = 0;
        StartTDEvent(key);
    }
    //会話の間を持たせる関数
    IEnumerator StopTalk(string key)
    {
        StartCoroutine(Seconds3Talk());
        yield return new WaitForSeconds(1);
    }
    IEnumerator Seconds3Talk()
    {
        isTalkStop = true;
        Time.timeScale = 0;
        yield return new WaitForSeconds(3);
        Time.timeScale = 1;
        isTalkStop = false;
    }
    //話の最中に他の出来事が起こり、NDialogで話を展開するためのもの
    //public isTalkStopがtrueでクリックしてもTDialogが反応しないためNDialogを表示できる
    //public IEnumerator StopTalkUntill(string key)
    //{
    //    yield return new WaitUntil(() =>
    //    {
    //        if (key == "TDEventTag1-A")
    //        {
    //            isTalkStop = true;
    //            StartTDEvent("Guooo!!!");
    //            return isTalkStop == false;
    //        }
    //        else
    //        {
    //            return true;
    //        }
    //    });
    //}
}
