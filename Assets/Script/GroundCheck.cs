using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool isGround=false;
    private bool isGroundEnter, isGroundStay,isGroundExit;

    public int IsGround(int maxjump,int restjump) //�W�����v�̍ő�񐔂ƍ��W�����v�ł���񐔂�n���Ēn�ʂɕt������W�����v�񕜁A���ĂȂ��Ȃ�c��̃W�����v�񐔂�Ԃ�
    {
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        } else if (isGroundExit) 
        {
            isGround= false;
        }
        isGroundEnter= false;
        isGroundStay= false;
        isGroundExit= false;
        if (isGround)
        {
            return maxjump;
        }
        else 
        {
            return restjump;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name=="TilemapColleider") 
        {
            isGroundEnter= true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name=="TilemapCollider") 
        {
            isGroundStay= true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.name == "TilemapCollider") 
        {
            isGroundExit= true;
        }
    }
}
