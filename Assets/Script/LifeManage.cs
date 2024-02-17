using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManage : MonoBehaviour
{
    public int maxLife = 5; // ライフの最大値
    private int currentLife; // 現在のライフ
    public GameObject heartPrefab; // ハートのプレハブ
    public GameObject heartAreaPrefab; // ユニークなオブジェクトのプレハブ

    private void Start()
    {
        int objectCount = FindObjectsOfType<LifeManage>().Length;
        if (objectCount == 1)
        // ハートの初期化処理を実行
        currentLife = maxLife;
        InitializeHearts();
    }

    void InitializeHearts()
    {
        float heartWidth = 30f; // ハートの幅
        float startPosition = -850f;

        for (int i = 0; i < maxLife; i++)
        {
            GameObject heart = Instantiate(heartPrefab, transform);
            heart.transform.localPosition = new Vector2(startPosition + i * (heartWidth + 40), 460f); // ハートの位置を設定
            heart.SetActive(true); // ハートをアクティブにする
        }

        UpdateHearts(); // ハートの表示を初期化する
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
                heartImage.enabled = true; // ハートを表示
            }
            else
            {
                heartImage.enabled = false; // ハートを非表示
            }
        }
    }

    void GameOver()
    {
        Debug.Log("GameOver");
        // どこかにゲームオーバーの処理なかったでしたっけ？移植してほしいです
    }

}
