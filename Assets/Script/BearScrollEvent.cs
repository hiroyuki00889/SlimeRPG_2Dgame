using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearScrollEvent : MonoBehaviour
{
    public GameObject bear;
    public Vector3 scrollspeed= new Vector3(0.01f, 0, 0);
    private float time = 0f;
    private void Update() 
    {
        time+=Time.deltaTime;
        this.transform.position += scrollspeed;
        if (time>3) 
        {
            bear.gameObject.transform.position +=scrollspeed;
        }
    }

}