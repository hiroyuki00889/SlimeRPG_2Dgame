using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterDisplay : MonoBehaviour
{
    public EnemyTagCounter enemyTagCounter;
    public Text counterText;

    private void Update()
    {
        // �J�E���^�[�̏����擾
        Dictionary<string, int> counters = enemyTagCounter.GetAllCounters();

        // �J�E���^�[�̒l��\��
        counterText.text = "";

        foreach (var kvp in counters)
        {
            counterText.text += $"{kvp.Key}: {kvp.Value}\n";
        }
    }
}
