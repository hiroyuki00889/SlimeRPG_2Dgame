using UnityEngine;
using System.Collections.Generic;

public class EnemyTagCounter : MonoBehaviour
{
    // �G�̃^�O�ƃJ�E���^�[�̎���
    private Dictionary<string, int> enemyTagCounters = new Dictionary<string, int>();

    // ������
    private void Start()
    {
        // ���������ɑS�ẴJ�E���^�[���[���ɐݒ�
        ResetCounters();
    }

    // �w�肵���G�̃^�O�ɑΉ�����J�E���^�[���C���N�������g
    public void IncrementCounter(string enemyTag)
    {
        if (enemyTagCounters.ContainsKey(enemyTag))
        {
            enemyTagCounters[enemyTag]++;
        }
    }

    // �w�肵���G�̃^�O�ɑΉ�����J�E���^�[���擾
    public int GetCounter(string enemyTag)
    {
        if (enemyTagCounters.ContainsKey(enemyTag))
        {
            return enemyTagCounters[enemyTag];
        }
        return 0; // �J�E���^�[�����݂��Ȃ��ꍇ��0��Ԃ�
    }


    public Dictionary<string, int> GetAllCounters()
    {
        return enemyTagCounters;
    }


    // �S�ẴJ�E���^�[���[���Ƀ��Z�b�g
    public void ResetCounters()
    {
        enemyTagCounters.Clear(); // �������N���A���ă[���Ƀ��Z�b�g
        // �����ŕK�v�ȓG�̃^�O�Ə����l��ǉ�
        //���݂�0�����Z�[�u�f�[�^����value���Q�Ƃł���悤�ɂ���΃J�E���^�[�̈��p�����\
        enemyTagCounters.Add("EnemyTagA", 0);
        enemyTagCounters.Add("EnemyTagB", 0);
        // ���̓G�̃^�O���K�v�ɉ����Ēǉ�
    }
}
