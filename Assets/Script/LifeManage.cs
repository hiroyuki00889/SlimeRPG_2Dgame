using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManage : MonoBehaviour
{
    private int life = 5;

    public void TakeDamage()
    {
        life--;
        Debug.Log(life);

        if(life <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("GameOver");
        // �ǂ����ɃQ�[���I�[�o�[�̏����Ȃ������ł��������H�ڐA���Ăق����ł�
    }

}
