using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveScript : MonoBehaviour
{
    public GameObject activetarget;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")) 
        {
            activetarget.SetActive(true);
        }
    }

    //��߂肳���Ȃ��悤��spriterenderer��Active(True)��Y��Ȃ�����
}
