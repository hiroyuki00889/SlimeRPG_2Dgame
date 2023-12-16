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
    private string[] parts;
    int i = 0;
    public bool isNDEvent;
    ////������`
    private Dictionary<string, string> ndEvent = new Dictionary<string, string>();

    private void Start()
    {
        //��b���ǉ��B�u,,�v�ŕ��͂̋�؂�B�^�O��Unity�̕��ƍ��킹��
        ndEvent.Add("NDEventTag1-A", "�����̓꒣��ɏZ�ރX���C���͕��a�ɕ�炵�Ă��܂���,,�������A���邱�Ƃœ꒣������߂�l�V���Ƀ��J���Ă��܂��܂���,,�X���C���͎l�V����|�����ɏo�܂�");
        ndEvent.Add("NDEventTagB", "�C�x���g�^�OB�̕��͂ł�");
        ndEvent.Add("NDEventTagC", "�C�x���g�^�OC�̕��͂ł�");
        ndEvent.Add("NDEventTagD", "�C�x���g�^�OD�̕��͂ł�");
        //Stage1����N�����̕\�����́A���s��Stage1�X�N���v�g
        ndEvent.Add("Stage1", "�����̓꒣��ɏZ�ރX���C���͕��a�ɕ�炵�Ă��܂���,,�������A���邱�Ƃœ꒣������߂�l�V���Ƀ��J���Ă��܂��܂���,,�X���C���͎l�V����|�����ɏo�܂�");
    }

    public void StartNDEvent(string eventTag)
    {
        // �R���C�_�[�̃^�O�ŉ�b���e���擾
        if (ndEvent.TryGetValue(eventTag, out string ndEventText))
        {
            // ��b���e��\��
            DisplayNDEvent(ndEventText);
        }
        else
        {
            //�G���[�n���h�����O�̓��e�������ɋL�q
            Debug.Log("�i���[�^�[�_�C�A���O�̃^�O�����͂̎�������肭�Q�Ƃ���Ă��Ȃ�");
        }
    }

    void DisplayNDEvent(string text)
    {
        string delimiter = ",,"; //��؂蕶��
        //��؂蕶���ŋ�؂����p�[�g��z��ɓ���鏈��
        parts = text.Split(new[] { delimiter }, StringSplitOptions.None);
        //�ŏ��̕��͓���
        ndEventText.text = parts[0];
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //��x�����Ăяo����Ȃ��t���O��false���A�Փ˂��Ă������肪Player
        if (!isCallOnece && collider.gameObject.CompareTag("Player"))
        {
            isCallOnece = true;           
            isNDEvent = true;
        }
    }

    private void Update()
    {
        //�i���[�^�[�_�C�A���O���J���Ă���ԁA���N���b�N�������ƁA���͑���
        if (animateNDialog.IsOpen && animateNDialog.n_SentenceTrigger == true)
        {
            if (i < parts.Length - 1)
            {
                i++;
                ndEventText.text = parts[i];
                animateNDialog.n_SentenceTrigger = false;�@//�N���b�N�������Ȃ��Ă����͑��肳���̂�h��
            }

            //�Ō�̕��͂ɂȂ��āA���N���b�N�������ƃt���O�I�t�AText�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            if (i >= parts.Length - 1)
            {
                isNDEvent = false;
                i=0;
            }
        }
    }
}
