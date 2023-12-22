using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    [SerializeField] AnimateNDialog animateNDialog;
    [SerializeField] NDEvent ndEvent;
    //辞書のキーと同じ文字列で判定
    
    string sd = "Skill";
    void Start()
    {
        //何回も繰り返し見るデバック用処理
        PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("FirstStage1"))
        {
            string tag = "Stage1";
            //初回起動時の処理を実行
            ndEvent.isNDEvent = true;
            animateNDialog.DialogNarratorOpen();
            ndEvent.StartNDEvent(tag);

            //Stage1キーに値を入れる
            PlayerPrefs.SetInt("FirstStage1", 1);
            PlayerPrefs.Save();
            
        }
    }
    //最初の犬を倒したときにスキルなどの説明ダイアログ
    void OnDisable()
    {
        if (!PlayerPrefs.HasKey("SkillDiscribe"))
        {
            //スキル説明
            ndEvent.isNDEvent = true;
            animateNDialog.DialogNarratorOpen();
            ndEvent.StartNDEvent(sd);

            //Stage1キーに値を入れる
            PlayerPrefs.SetInt("Skilldiscribe", 1);
            PlayerPrefs.Save();
        }
        //犬を無視して行こうとするプレイヤーに対する処理を何処かに書く
        Debug.Log("OnDisable");
    }
}
