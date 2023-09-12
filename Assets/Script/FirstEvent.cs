using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstEvent : MonoBehaviour
{
    [SerializeField] AnimateNDialog animateNDialog;
    bool isCallOnece = false;
    public Text firstEventText;
    public bool isFirstEvent = false;
    int i = 0;

    string[] words = {"魔物の縄張りに住むスライムは平和に暮らしていました",
        "しかし、あることで縄張りを治める四天王にムカついてしまいました","スライムは四天王を倒す旅に出ます"};

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //一度しか呼び出されないフラグがfalseかつ、衝突してきた相手がPlayer
        if (!isCallOnece && collider.gameObject.CompareTag("Player"))
        {
            isCallOnece = true;
            firstEventText.text = words[i];
            isFirstEvent = true;
        }
    }

    private void Update()
    {
        //ナレーターダイアログが開いている間、エンターキーを押すと、文章送り
        if(animateNDialog.IsOpen && animateNDialog.enterTrigger == true)
        {
            if (i <= words.Length)
            {
                i++;
                firstEventText.text = words[i];
                animateNDialog.enterTrigger = false;
            }

            //最後の文章になって、エンターキーを押すとフラグオフ、Textオブジェクトを非アクティブにする
            if(i >= words.Length && animateNDialog.enterTrigger == true) 
            {
                isFirstEvent = false;
                gameObject.SetActive(false);
            }
        }
    }
}
