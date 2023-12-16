using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BearScrollEvent : MonoBehaviour
{
    public GameObject bear;
    public Vector3 scrollspeed= new Vector3(0.01f, 0, 0);
    public Vector3 upspeed;
    private float time = 0f;
    private float speeddowntime = 0f;   
    private bool a;
    private void Update() 
    {
        time+=Time.deltaTime;
        this.transform.position += scrollspeed;
        if (this.transform.position.x < 5)
        {
            BearSpeedUp(); //熊のスピードが上がる
        }
        else if (this.transform.position.x < 10)
        {
            BearSpeedDown();
        } else if (this.transform.position.x < 15 && !a) 
        {
            a = true;
            SpeedUp();
        }
    }


    public void SpeedUp() 
    {
        Debug.Log("speedup");
        scrollspeed*=1.5f;
    }

    public void BearSpeedUp() 
    {
        bear.gameObject.transform.position += scrollspeed;
        speeddowntime += Time.deltaTime;
    }

    public void BearSpeedDown()
    {
            bear.gameObject.transform.position -= scrollspeed; 
    }
}