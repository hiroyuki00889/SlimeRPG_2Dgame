using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NDEvent : MonoBehaviour
{

    [SerializeField] AnimateNDialog animateNDialog;
    bool isCallOnece = false;
    public Text ndEventText;
    public bool isNDEvent = false;
    public bool FirstEvent; //�`�F�b�N����Ȃ��ŕ\�����镶�͂�I��
    private bool[] hantei; �@//����p�ϐ��z��

    string[] words = {"�����̓꒣��ɏZ�ރX���C���͕��a�ɕ�炵�Ă��܂���",
        "�������A���邱�Ƃœ꒣������߂�l�V���Ƀ��J���Ă��܂��܂���","�X���C���͎l�V����|�����ɏo�܂�"};

    private void Start()
    {
        //����p�ϐ��z���bool�l����
        /*hantei =FirstEvent;*/
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //��x�����Ăяo����Ȃ��t���O��false���A�Փ˂��Ă������肪Player
        if (!isCallOnece && collider.gameObject.CompareTag("Player"))
        {
            CoiceSentence();
        }
    }

    private void CoiceSentence()
    {
        isCallOnece = true;
        //����p�ϐ��z��̗v�f�����Ԃɔ���
        /*for (int i  = 0; i <= hantei.Length-1; i++)
        {
            if ()
            {

            }
            if else (){

            }
        }*/
        isNDEvent = true;
    }

    private void Update()
    {
        //�i���[�^�[�_�C�A���O���J���Ă���ԁA���N���b�N�������ƁA���͑���
        if (animateNDialog.IsOpen && animateNDialog.n_SentenceTrigger == true)
        {
            int i = 0;
            if (i < words.Length - 1)
            {
                i++;
                ndEventText.text = words[i];
                animateNDialog.n_SentenceTrigger = false;
            }

            //�Ō�̕��͂ɂȂ��āA���N���b�N�������ƃt���O�I�t�AText�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            if (i >= words.Length - 1 && animateNDialog.n_SentenceTrigger == true)
            {
                isNDEvent = false;
                gameObject.SetActive(false);
            }
        }
    }
}
