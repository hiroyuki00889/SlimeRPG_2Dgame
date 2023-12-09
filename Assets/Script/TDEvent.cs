using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TDEvent : MonoBehaviour
{
    [SerializeField] AnimateTDialog animateTDialog;
    bool isCallOnece = false;
    public Text nameText;
    public Text tdEventText;
    string[] parts;
    int i = 1;
    public bool isTDEvent = false;
    ////������`
    private Dictionary<string, string> ndEvent = new Dictionary<string, string>();

    private void Start()
    {
        //��b���ǉ��B�u,,�v�Ŗ��O�ƕ��͂̋�؂�B�ŏ��͖��O�BUnity�̕��̃^�O�ƍ��킹��
        ndEvent.Add("TDEventTag1-A", "���,,�I�b�X�A�I�����I,,���̐��E�ɂ͂��[����������񂢂�񂾂�,,�I�����N���N�������I");
        ndEvent.Add("TDEventTagB", "�C�x���g�^�OB�̕��͂ł�");
        ndEvent.Add("TDEventTagC", "�C�x���g�^�OC�̕��͂ł�");
        ndEvent.Add("TDEventTagD", "�C�x���g�^�OD�̕��͂ł�");
        // �����ɉ�b���ǉ�
    }

    public void StartTDEvent(string eventTag)
    {
        // �R���C�_�[�̃^�O�ŉ�b���e���擾
        if (ndEvent.TryGetValue(eventTag, out string tdEventText))
        {
            // ��b���e��\��
            DisplayTDEvent(tdEventText);
        }
        else
        {
            //�G���[�n���h�����O�̓��e�������ɋL�q
            Debug.Log("�g�[�N�_�C�A���O�̃^�O�����͂̎�������肭�Q�Ƃ���Ă��Ȃ�");
        }
    }

    void DisplayTDEvent(string text)
    {
        string delimiter = ",,"; //��؂蕶��
        //��؂蕶���ŋ�؂����p�[�g��z��ɓ���鏈��
        parts = text.Split(new[] { delimiter }, StringSplitOptions.None);
        //���O����
        nameText.text = parts[0];
        //�ŏ��̕��͓���
        tdEventText.text = parts[1];
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //��x�����Ăяo����Ȃ��t���O��false���A�Փ˂��Ă������肪Player
        if (!isCallOnece && collider.gameObject.CompareTag("Player"))
        {
            isCallOnece = true;
            isTDEvent = true;
        }
    }

    private void Update()
    {
        //�i���[�^�[�_�C�A���O���J���Ă���ԁA���N���b�N�������ƁA���͑���
        if (animateTDialog.t_IsOpen && animateTDialog.t_SentenceTrigger == true)
        {
            if (i < parts.Length - 1)
            {
                i++;
                tdEventText.text = parts[i];
                animateTDialog.t_SentenceTrigger = false;�@//�N���b�N�������Ȃ��Ă����͑��肳���̂�h��
            }

            //�Ō�̕��͂ɂȂ��āA���N���b�N�������ƃt���O�I�t�AText�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            if (i >= parts.Length - 1)
            {
                isTDEvent = false;
                i = 0;
            }
        }
    }
}
