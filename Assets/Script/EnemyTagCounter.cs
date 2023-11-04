using UnityEngine;
using System.Collections.Generic;

public class EnemyTagCounter : MonoBehaviour
{
    public Dictionary<string, int> enemyTagCounters = new Dictionary<string, int>();
    private Dictionary<string, int> previousCounters = new Dictionary<string, int>(); // �ǉ�


    private void Start()
    {
        ResetCounters();
    }

    // �X�L���J�E���^�[����
    public void IncrementCounter(string enemyTag)
    {
        if (enemyTagCounters.ContainsKey(enemyTag))
        {
            enemyTagCounters[enemyTag]++;
        }
    }

        // �X�L���J�E���^�[����
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

        // �ǉ�: ���������ɑO��̃J�E���^�[���X�V
        //foreach (var kvp in enemyTagCounters)
        /*{
            previousCounters[kvp.Key] = kvp.Value;
        }*/
    }

    // �ǉ�: �J�E���^�[���ύX���ꂽ���ǂ������m�F����֐�
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
