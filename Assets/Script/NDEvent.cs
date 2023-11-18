using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NDEvent : MonoBehaviour
{

    [SerializeField] AnimateNDialog animateNDialog;
    bool isCallOnece = false;
    public Text ndEventText;
    public bool isNDEvent = false;
    public bool FirstEvent; //チェックありなしで表示する文章を選択
    private bool[] hantei; 　//判定用変数配列

    string[] words = {"魔物の縄張りに住むスライムは平和に暮らしていました",
        "しかし、あることで縄張りを治める四天王にムカついてしまいました","スライムは四天王を倒す旅に出ます"};

    private void Start()
    {
        //判定用変数配列にbool値を代入
        /*hantei =FirstEvent;*/
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //一度しか呼び出されないフラグがfalseかつ、衝突してきた相手がPlayer
        if (!isCallOnece && collider.gameObject.CompareTag("Player"))
        {
            CoiceSentence();
        }
    }

    private void CoiceSentence()
    {
        isCallOnece = true;
        //判定用変数配列の要素を順番に判定
        /*for (int i  = 0; i <= hantei.Length-1; i++)
        {
            if ()
            {

            }
            if else (){

            }
        }*/
        isNDEvent = true;
    }

    private void Update()
    {
        //ナレーターダイアログが開いている間、左クリックを押すと、文章送り
        if (animateNDialog.IsOpen && animateNDialog.n_SentenceTrigger == true)
        {
            int i = 0;
            if (i < words.Length - 1)
            {
                i++;
                ndEventText.text = words[i];
                animateNDialog.n_SentenceTrigger = false;
            }

            //最後の文章になって、左クリックを押すとフラグオフ、Textオブジェクトを非アクティブにする
            if (i >= words.Length - 1 && animateNDialog.n_SentenceTrigger == true)
            {
                isNDEvent = false;
                gameObject.SetActive(false);
            }
        }
    }
}
