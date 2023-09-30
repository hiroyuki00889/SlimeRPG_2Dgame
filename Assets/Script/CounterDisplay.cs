using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CounterDisplay : MonoBehaviour
{
    public EnemyTagCounter enemyTagCounter;
    public Text counterTextA; // カウンターAを表示するUIテキスト
    public Text counterTextB; // カウンターBを表示するUIテキスト
    public Image Club;
    public Image Axe;



    private void Update()
    {
        // カウンターの情報を取得
        Dictionary<string, int> counters = enemyTagCounter.GetAllCounters();

        // カウンターの値をUIテキストに表示

        foreach (var kvp in counters)
        {
            if (kvp.Key == "EnemyTagA")
            {
                if (kvp.Value == 0)
                {
                    Club.gameObject.SetActive(false); // 画像を非表示
                    counterTextA.gameObject.SetActive(false);  // テキストを非表示
                }
                else if (kvp.Value >= 1)
                {
                    Club.gameObject.SetActive(true);  // 画像を表示
                    counterTextA.gameObject.SetActive(true);   // テキストを表示

                    // テキストに数値を表示
                    counterTextA.text = kvp.Value.ToString();
                }
                // counterTextA.text = $"EnemyTagA: {kvp.Value}";
            }
            else if (kvp.Key == "EnemyTagB")
            {
                if (kvp.Value == 0)
                {
                    Axe.gameObject.SetActive(false); // 画像を非表示
                    counterTextB.gameObject.SetActive(false);  // テキストを非表示
                }
                else if (kvp.Value >= 1)
                {
                    Axe.gameObject.SetActive(true);  // 画像を表示
                    counterTextB.gameObject.SetActive(true);   // テキストを表示

                    // テキストに数値を表示
                    counterTextB.text = kvp.Value.ToString();
                }
                // counterTextB.text = $"EnemyTagB: {kvp.Value}";
            }
            // 他のタグに対する表示も追加できます
        }
    }
}





