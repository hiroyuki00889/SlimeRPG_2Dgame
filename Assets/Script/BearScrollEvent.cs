using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearScrollEvent : MonoBehaviour
{
    public GameObject background;
    private void Update() 
    {
        background.transform.position += new Vector3(0.01f,0,0);
    }
}