using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstEvent : MonoBehaviour
{
    [SerializeField] AnimateNDialog animateNDialog;
    bool isCallOnece = false;
    public Text firstEventText;
    public bool isFirstEvent = false;
    int i = 0;

    string[] words = {"�����̓꒣��ɏZ�ރX���C���͕��a�ɕ�炵�Ă��܂���",
        "�������A���邱�Ƃœ꒣������߂�l�V���Ƀ��J���Ă��܂��܂���","�X���C���͎l�V����|�����ɏo�܂�"};

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //��x�����Ăяo����Ȃ��t���O��false���A�Փ˂��Ă������肪Player
        if (!isCallOnece && collider.gameObject.CompareTag("Player"))
        {
            isCallOnece = true;
            firstEventText.text = words[i];
            isFirstEvent = true;
        }
    }

    private void Update()
    {
        //�i���[�^�[�_�C�A���O���J���Ă���ԁA�G���^�[�L�[�������ƁA���͑���
        if(animateNDialog.IsOpen && animateNDialog.enterTrigger == true)
        {
            if (i <= words.Length)
            {
                i++;
                firstEventText.text = words[i];
                animateNDialog.enterTrigger = false;
            }

            //�Ō�̕��͂ɂȂ��āA�G���^�[�L�[�������ƃt���O�I�t�AText�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            if(i >= words.Length && animateNDialog.enterTrigger == true) 
            {
                isFirstEvent = false;
                gameObject.SetActive(false);
            }
        }
    }
}
