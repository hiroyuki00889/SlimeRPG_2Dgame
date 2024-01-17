using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using Unity.VisualScripting;

public class BearScrollEvent : MonoBehaviour
{
    public GameObject parent;
    public Vector3 scrollspeed= new Vector3(0.01f, 0, 0); //スクロールスピード
    public Vector3 upspeed; //スピードアップ時の速度
    private float speeddowntime = 0f;
    private bool a;
    private Animator bearanim;

    private int roat = Animator.StringToHash("Roat");
    private int stay = Animator.StringToHash("Stay");
    private int bearbreak = Animator.StringToHash("Break");

    [SerializeField] private float[] eventcoordinate; //イベントの座標
    private int i = 0;
    [SerializeField] private UnityEvent[] events; //イベントの座標に到達した時何のイベントを起こすか

    private bool staynow;

    private void Start()
    {
        bearanim = GetComponent<Animator>();
    }

    private void Update() 
    {
        if (!staynow) 
        {
            parent.transform.position += scrollspeed; //現在は親オブジェクトの座標準拠(熊そのものだとワールド座標に変換する処理が入りそうなため)

            if (parent.transform.position.x > eventcoordinate[i]) 
            {
                events[i].Invoke();
                i++;
            }
        }
    }


    public void SpeedUp() 
    {
        scrollspeed*=1.5f;
    }

    public void BearSpeedUp() //スピードダウンを必ず入れるなら、引数で秒数指定して呼び出す
    {
        this.gameObject.transform.position += scrollspeed;
    }

    public void BearSpeedDown()
    {
        this.gameObject.transform.position -= scrollspeed;
    }

    public void BearStay(float time) //指定された秒数待つ
    {
        StartCoroutine(BearStayTime(time));
    }

    public void BearRoat()
    {
        bearanim.SetTrigger(roat);
        BearStay(2f); //後隙　敵が集まってくるまでの時間を指定
    }

    public void BearBreak()
    {
        bearanim.SetTrigger(bearbreak);
        //エフェクトや判定など
    }

    private IEnumerator BearStayTime(float time)
    {
        bearanim.SetBool(stay, true);
        staynow = true;
        yield return new WaitForSeconds(time);
        staynow = false;
        bearanim.SetBool(stay, false);
    }

    public void DiagonalUp(float time) 
    {
        StartCoroutine(Diagonal(time,true));
    }

    public void DiagonalDown(float time)
    {
        StartCoroutine(Diagonal(time, false));
    }

    private IEnumerator Diagonal(float time,bool b)//bool値は上がるか下がるか trueで上がる
    {
        if (b)
        {
            scrollspeed.y = 0.01f; //どれくらい上がるか(下がるか)
        }
        else 
        {
            scrollspeed.y = -0.01f;
        }
        yield return new WaitForSeconds(time);
        scrollspeed.y = 0;
    }

    //やられた時の処理
}