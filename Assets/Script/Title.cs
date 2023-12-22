using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] AnimateNDialog animateNDialog;
    [SerializeField] NDEvent ndEvent;
    private string title = "Title1";
    void Start()
    {
        //何回も繰り返し見るデバック用処理
        PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("Title"))
        {
            Debug.Log("ハスキー");
            //初回起動時の処理を実行
            ndEvent.isNDEvent = true;
            animateNDialog.DialogNarratorOpen();
            ndEvent.StartNDEvent(title);

            //Stage1キーに値を入れる
            PlayerPrefs.SetInt("Title", 1);
            PlayerPrefs.Save();

        }
    }
}
