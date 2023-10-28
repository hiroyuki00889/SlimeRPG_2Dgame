using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CounterDisplay : MonoBehaviour
{
    public EnemyTagCounter enemyTagCounter;
    public Text counterTextBunny; // �J�E���^�[A��\������UI�e�L�X�g
    public Text counterTextB; // �J�E���^�[B��\������UI�e�L�X�g
    public Image Club;
    public Image Axe;
    public Button Bunny;



    private void Update()
    {
        // �J�E���^�[�̏����擾
        Dictionary<string, int> counters = enemyTagCounter.GetAllCounters();

        // �J�E���^�[�̒l��UI�e�L�X�g�ɕ\��
        foreach (var kvp in counters)
        {
            if (kvp.Key == "Bunny")
            {
                if (kvp.Value == 0)
                {
                    //Club.gameObject.SetActive(false); // �摜���\��
                    Bunny.interactable = false;
                    counterTextBunny.gameObject.SetActive(false);  // �e�L�X�g���\��
                }
                else if (kvp.Value >= 1)
                {
                    //Club.gameObject.SetActive(true);  // �摜��\��
                    Bunny.interactable = true;
                    counterTextBunny.gameObject.SetActive(true);   // �e�L�X�g��\��

                    // �e�L�X�g�ɐ��l��\��
                    counterTextBunny.text = kvp.Value.ToString();
                }
                // counterTextA.text = $"EnemyTagA: {kvp.Value}";
            }
            else if (kvp.Key == "EnemyTagB")
            {
                if (kvp.Value == 0)
                {
                    Axe.gameObject.SetActive(false); // �摜���\��
                    counterTextB.gameObject.SetActive(false);  // �e�L�X�g���\��
                }
                else if (kvp.Value >= 1)
                {
                    Axe.gameObject.SetActive(true);  // �摜��\��
                    counterTextB.gameObject.SetActive(true);   // �e�L�X�g��\��

                    // �e�L�X�g�ɐ��l��\��
                    counterTextB.text = kvp.Value.ToString();
                }
                // counterTextB.text = $"EnemyTagB: {kvp.Value}";
            }
            // ���̃^�O�ɑ΂���\�����ǉ��ł��܂�
        }
    }
}





