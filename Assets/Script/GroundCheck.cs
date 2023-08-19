using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool isGround=false;
    private bool isGroundEnter, isGroundStay,isGroundExit;
    private bool groundNow = true;
    private string ground_tag = "Ground";

    public int IsGround(int maxjump,int restjump) //���W�����v�ł���񐔂�n���Ēn�ʂɕt������W�����v�񕜁A���ĂȂ��Ȃ�c��̃W�����v�񐔂�Ԃ�
    {
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }
        else if (isGroundExit) 
        {
            isGround= false;
        }
        isGroundEnter= false;
        isGroundStay= false;
        isGroundExit= false;
        if (isGround)
        {
            groundNow = true;
            return maxjump;
        }
        else 
        {
            if (groundNow) 
            {
                groundNow = false;
                return restjump - 1;
            }
            return restjump;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag==ground_tag) 
        {
            isGroundEnter= true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag==ground_tag)
        {
            isGroundStay= true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.tag == ground_tag) 
        {
            isGroundExit= true;
        }
    }
}
