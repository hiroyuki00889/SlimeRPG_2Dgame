using UnityEngine;
using System.Collections.Generic;

public class EnemyTagCounter : MonoBehaviour
{
    public Dictionary<string, int> enemyTagCounters = new Dictionary<string, int>();
    private Dictionary<string, int> previousCounters = new Dictionary<string, int>(); // 追加


    private void Start()
    {
        ResetCounters();
    }

    // スキルカウンター増加
    public void IncrementCounter(string enemyTag)
    {
        if (enemyTagCounters.ContainsKey(enemyTag))
        {
            enemyTagCounters[enemyTag]++;
        }
    }

        // スキルカウンター減少
    public void DecrementCounter(string enemyTag)
    {
        if (enemyTagCounters.ContainsKey(enemyTag))
        {
            enemyTagCounters[enemyTag]--;
        }
    }

    public int GetCounter(string enemyTag)
    {
        if (enemyTagCounters.ContainsKey(enemyTag))
        {
            return enemyTagCounters[enemyTag];
        }
        return 0;
    }

    public Dictionary<string, int> GetAllCounters()
    {
        return enemyTagCounters;
    }

    public void ResetCounters()
    {
        enemyTagCounters.Clear();
        enemyTagCounters.Add("Bunny", 0);
        enemyTagCounters.Add("Dog", 0);

        // 追加: 初期化時に前回のカウンターも更新
        //foreach (var kvp in enemyTagCounters)
        /*{
            previousCounters[kvp.Key] = kvp.Value;
        }*/
    }

    // 追加: カウンターが変更されたかどうかを確認する関数
    public bool IsCounterChanged()
    {
        foreach (var kvp in enemyTagCounters)
        {
            if (kvp.Value != previousCounters[kvp.Key])
            {
                return true;
            }
        }
        return false;
    }
}
