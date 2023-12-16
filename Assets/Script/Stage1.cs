using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    [SerializeField] AnimateNDialog animateNDialog;
    [SerializeField] NDEvent ndEvent;
    void Start()
    {
        //何回も繰り返し見るデバック用処理
        PlayerPrefs.DeleteAll();
        //辞書のキーと同じ文字列で判定
        string tag = "Stage1";
        if (!PlayerPrefs.HasKey("FirstStage1"))
        {
            //初回起動時の処理を実行
            ndEvent.isNDEvent = true;
            animateNDialog.DialogNarratorOpen();
            ndEvent.StartNDEvent(tag);

            //Stage1キーに値を入れる
            PlayerPrefs.SetInt("FirstStage1", 1);
            PlayerPrefs.Save();
            
        }
    }

}
