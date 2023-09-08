using UnityEngine;
using System.Collections.Generic;

public class EnemyTagCounter : MonoBehaviour
{
    // 敵のタグとカウンターの辞書
    private Dictionary<string, int> enemyTagCounters = new Dictionary<string, int>();

    // 初期化
    private void Start()
    {
        // 初期化時に全てのカウンターをゼロに設定
        ResetCounters();
    }

    // 指定した敵のタグに対応するカウンターをインクリメント
    public void IncrementCounter(string enemyTag)
    {
        if (enemyTagCounters.ContainsKey(enemyTag))
        {
            enemyTagCounters[enemyTag]++;
        }
    }

    // 指定した敵のタグに対応するカウンターを取得
    public int GetCounter(string enemyTag)
    {
        if (enemyTagCounters.ContainsKey(enemyTag))
        {
            return enemyTagCounters[enemyTag];
        }
        return 0; // カウンターが存在しない場合は0を返す
    }


    public Dictionary<string, int> GetAllCounters()
    {
        return enemyTagCounters;
    }


    // 全てのカウンターをゼロにリセット
    public void ResetCounters()
    {
        enemyTagCounters.Clear(); // 辞書をクリアしてゼロにリセット
        // ここで必要な敵のタグと初期値を追加
        //現在は0だがセーブデータからvalueを参照できるようにすればカウンターの引継ぎが可能
        enemyTagCounters.Add("EnemyTagA", 0);
        enemyTagCounters.Add("EnemyTagB", 0);
        // 他の敵のタグも必要に応じて追加
    }
}
