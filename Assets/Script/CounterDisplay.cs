using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterDisplay : MonoBehaviour
{
    public EnemyTagCounter enemyTagCounter;
    public Text counterTextA; // �J�E���^�[A��\������UI�e�L�X�g
    public Text counterTextB; // �J�E���^�[B��\������UI�e�L�X�g

    private void Update()
    {
        // �J�E���^�[�̏����擾
        Dictionary<string, int> counters = enemyTagCounter.GetAllCounters();

        // �J�E���^�[�̒l��UI�e�L�X�g�ɕ\��
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
            // ���̃^�O�ɑ΂���\�����ǉ��ł��܂�
        }
    }
}





