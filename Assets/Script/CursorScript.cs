using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public EnemyTagCounter counter;
    public float minpos;
    public float maxpos;
    public float grid;
    
    //�܂����Ԗڂɉ��̃X�L��������̂�������Ȃ���� ��������

    void Update()
    {
        maxpos=counter.GetAllCounters().Count * grid;
        if (Input.GetKeyDown(KeyCode.LeftArrow) && this.transform.position.x > minpos)
        {
            this.transform.position -= new Vector3(grid,0,0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && this.transform.position.x < maxpos) 
        {
            this.transform.position += new Vector3(grid, 0, 0);
        }

    }

    public string GetSkillNow()
    {
        if (this.transform.position.x/grid ==0) //�A���J�[�|�W�V�������l���������̂ɂ���K�v����
        {
            return "";
        }
        else //�X�L���̎c�ʂ������ꍇ�ɕԂ�����
        {
            return "";
        }
    }
}
