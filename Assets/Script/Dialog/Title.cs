using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] NDTDEvent ndtdEvent;
    void Start()
    {
            //初回起動時の処理を実行
            ndtdEvent.Title();
    }
}
