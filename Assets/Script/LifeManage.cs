using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManage : MonoBehaviour
{
    public int maxLife = 5; // ���C�t�̍ő�l
    private int currentLife; // ���݂̃��C�t
    public GameObject heartPrefab; // �n�[�g�̃v���n�u


    private void Start()
    {
        currentLife = maxLife;
        InitializeHearts();
    }

    void InitializeHearts()
    {
        float heartWidth = 30f; // �n�[�g�̕�
        float startPosition = -480f;

        for (int i = 0; i < maxLife; i++)
        {
            GameObject heart = Instantiate(heartPrefab, transform);
            heart.transform.localPosition = new Vector2(startPosition + i * (heartWidth + 10), 220f); // �n�[�g�̈ʒu��ݒ�
            heart.SetActive(true); // �n�[�g���A�N�e�B�u�ɂ���
        }

        UpdateHearts(); // �n�[�g�̕\��������������
    }
    public void TakeDamage()
    {
        if (currentLife > 0)
        {
            currentLife--;
            UpdateHearts();
            Debug.Log(currentLife);
        }

        if(currentLife <= 0)
        {
            GameOver();
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Image heartImage = transform.GetChild(i).GetComponent<Image>();
            if (i < currentLife)
            {
                heartImage.enabled = true; // �n�[�g��\��
            }
            else
            {
                heartImage.enabled = false; // �n�[�g���\��
            }
        }
    }

    void GameOver()
    {
        Debug.Log("GameOver");
        // �ǂ����ɃQ�[���I�[�o�[�̏����Ȃ������ł��������H�ڐA���Ăق����ł�
    }

}
