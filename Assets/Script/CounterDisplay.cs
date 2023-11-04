using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CounterDisplay : MonoBehaviour
{
    public EnemyTagCounter enemyTagCounter;
    public Text counterTextBunny;
    public Text counterTextDog;
    public Text counterTextBat;
    public Button Bunny;
    public Button Dog;
    public Button Bat;



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
            else if (kvp.Key == "Dog")
            {
                if (kvp.Value == 0)
                {
                    //Club.gameObject.SetActive(false); // �摜���\��
                    Dog.interactable = false;
                    counterTextDog.gameObject.SetActive(false);  // �e�L�X�g���\��
                }
                else if (kvp.Value >= 1)
                {
                    //Club.gameObject.SetActive(true);  // �摜��\��
                    Dog.interactable = true;
                    counterTextDog.gameObject.SetActive(true);   // �e�L�X�g��\��

                    // �e�L�X�g�ɐ��l��\��
                    counterTextDog.text = kvp.Value.ToString();
                }
                // counterTextB.text = $"EnemyTagB: {kvp.Value}";
            }
            else if (kvp.Key == "Bat")
            {
                if (kvp.Value == 0)
                {
                    //Club.gameObject.SetActive(false); // �摜���\��
                    Bat.interactable = false;
                    counterTextBat.gameObject.SetActive(false);  // �e�L�X�g���\��
                }
                else if (kvp.Value >= 1)
                {
                    //Club.gameObject.SetActive(true);  // �摜��\��
                    Bat.interactable = true;
                    counterTextBat.gameObject.SetActive(true);   // �e�L�X�g��\��

                    // �e�L�X�g�ɐ��l��\��
                    counterTextBat.text = kvp.Value.ToString();
                }
                // counterTextB.text = $"EnemyTagB: {kvp.Value}";
            }
            // ���̃^�O�ɑ΂���\�����ǉ��ł��܂�
        }
    }
}





