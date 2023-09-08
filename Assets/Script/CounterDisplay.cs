using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterDisplay : MonoBehaviour
{
    public EnemyTagCounter enemyTagCounter;
    public Text counterText;

    private void Update()
    {
        // カウンターの情報を取得
        Dictionary<string, int> counters = enemyTagCounter.GetAllCounters();

        // カウンターの値を表示
        counterText.text = "";

        foreach (var kvp in counters)
        {
            counterText.text += $"{kvp.Key}: {kvp.Value}\n";
        }
    }
}
