using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManage : MonoBehaviour
{
    public int maxLife = 5; // ���C�t�̍ő�l
    private int currentLife; // ���݂̃��C�t
    public GameObject heartPrefab; // �n�[�g�̃v���n�u
    public GameObject heartAreaPrefab; // ���j�[�N�ȃI�u�W�F�N�g�̃v���n�u

    private void Start()
    {
        int objectCount = FindObjectsOfType<LifeManage>().Length;
        if (objectCount == 1)
        // �n�[�g�̏��������������s
        currentLife = maxLife;
        InitializeHearts();
    }

    void InitializeHearts()
    {
        float heartWidth = 30f; // �n�[�g�̕�
        float startPosition = -850f;

        for (int i = 0; i < maxLife; i++)
        {
            GameObject heart = Instantiate(heartPrefab, transform);
            heart.transform.localPosition = new Vector2(startPosition + i * (heartWidth + 40), 460f); // �n�[�g�̈ʒu��ݒ�
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
