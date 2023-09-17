using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterDisplay : MonoBehaviour
{
    public EnemyTagCounter enemyTagCounter;
    public Text counterTextA; // カウンターAを表示するUIテキスト
    public Text counterTextB; // カウンターBを表示するUIテキスト

    private void Update()
    {
        // カウンターの情報を取得
        Dictionary<string, int> counters = enemyTagCounter.GetAllCounters();

        // カウンターの値をUIテキストに表示
        foreach (var kvp in counters)
        {
            if (kvp.Key == "EnemyTagA")
            {
                counterTextA.text = $"EnemyTagA: {kvp.Value}";
            }
            else if (kvp.Key == "EnemyTagB")
            {
                counterTextB.text = $"EnemyTagB: {kvp.Value}";
            }
            // 他のタグに対する表示も追加できます
        }
    }
}





