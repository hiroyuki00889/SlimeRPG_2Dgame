using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    [SerializeField] AnimateNDialog animateNDialog;
    [SerializeField] NDEvent ndEvent;
    //辞書のキーと同じ文字列で判定
    void Start()
    {
            string tag = "Stage1";
            //初回起動時の処理を実行
            ndEvent.isNDEvent = true;
            ndEvent.StartNDEvent(tag);

    }
    //最初の犬を倒したときにスキルなどの説明ダイアログ
    
}
