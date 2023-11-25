using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NDEvent : MonoBehaviour
{

    [SerializeField] AnimateNDialog animateNDialog;
    bool isCallOnece = false;
    public Text ndEventText;
    int i = 0;
    public bool isNDEvent = false;
    public bool FirstEvent;//�`�F�b�N����Ȃ��ŕ\�����镶�͂�I��

    string[] words = {"�����̓꒣��ɏZ�ރX���C���͕��a�ɕ�炵�Ă��܂���",
        "�������A���邱�Ƃœ꒣������߂�l�V���Ƀ��J���Ă��܂��܂���","�X���C���͎l�V����|�����ɏo�܂�"};

    ////������`
    //private Dictionary<string, string> ndEvent = new Dictionary<string, string>();

    //private void Start()
    //{
    //    ndEvent.Add("EventTagA", "�C�x���g�^�OA�̕��͂ł�");
    //    ndEvent.Add("EventTagB", "�C�x���g�^�OB�̕��͂ł�");
    //    ndEvent.Add("EventTagC", "�C�x���g�^�OC�̕��͂ł�");
    //    ndEvent.Add("EventTagD", "�C�x���g�^�OD�̕��͂ł�");
    //    // �����ɉ�b���ǉ�
    //}

    //public void StartNDEvent(string eventTag)
    //{
    //    // �R���C�_�[�̃^�O�ŉ�b���e���擾
    //    if(ndEvent.TryGetValue(eventTag, out string ndEventText))
    //    {
    //        // ��b���e��\��
    //        DisplayNDEvent(ndEventText);
    //    }
    //    //else
    //    //{
    //    //    �G���[�n���h�����O�̓��e�������ɋL�q
    //    //}
    //}

    //void DisplayNDEvent(string text)
    //{
    //    // ��b���e��UI�ɕ\�����鏈��
    //    ndEventText.text = text;
    //    // ���͂𑗂��ĕ\�����鏈�����ȉ��ɋL�q
    //    // �v�����܂���
    //}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //��x�����Ăяo����Ȃ��t���O��false���A�Փ˂��Ă������肪Player
        if (!isCallOnece && collider.gameObject.CompareTag("Player"))
        {
            isCallOnece = true;
            
            isNDEvent = true;
            CoiceSentence();
        }
    }

    private void CoiceSentence()
    {
        
        if (FirstEvent)
        {
            ndEventText.text = words[i];
        }
        
    }

    private void Update()
    {
        //�i���[�^�[�_�C�A���O���J���Ă���ԁA���N���b�N�������ƁA���͑���
        if (animateNDialog.IsOpen && animateNDialog.n_SentenceTrigger == true)
        {
            if (i < words.Length - 1)
            {
                i++;
                ndEventText.text = words[i];
                animateNDialog.n_SentenceTrigger = false;�@//�N���b�N�������Ȃ��Ă����͑��肳���̂�h��
            }

            //�Ō�̕��͂ɂȂ��āA���N���b�N�������ƃt���O�I�t�AText�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            if (i >= words.Length - 1)
            {
                isNDEvent = false;
            }
        }
    }
}
