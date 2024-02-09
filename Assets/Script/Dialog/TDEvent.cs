using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TDEvent : MonoBehaviour
{
    [SerializeField] AnimateTDialog animateTDialog;
    private string callOnece;
    public Text nameText;
    public Text tdEventText;
    string[] parts;
    int i = 1;
    public bool isTDEvent;
    public bool isTalkStop;
    ////������`
    private Dictionary<string, string> tdEvent = new Dictionary<string, string>();

    private void Start()
    {
        //��b���ǉ��B�u,,�v�Ŗ��O�ƕ��͂̋�؂�B�ŏ��͖��O�BUnity�̕��̃^�O�ƍ��킹��
        tdEvent.Add("TDEventTag1-A", "���C�o��,,�悤�I,,���O�����Ă邩�H,,�l�V������������x�z����1�N�A���V��ɂȂ��Ă₪��,,��������������ɂł��Ȃ����,,�ȂɁH���O���|�������āH,,�΂������A�����|���񂾂�I,,���O��艴�̕K�E�Z�̂��c,, ");
        tdEvent.Add("TDEventTag1-B", "���C�o��,,�C�x���g�^�OB�̕��͂ł�,,�P�s���ƃG���[�ɂȂ�̂�,,aaaaaaa,,iiiiii");
        tdEvent.Add("TDEventTagC", "�C�x���g�^�OC�̕��͂ł�");
        tdEvent.Add("TDEventTagD", "�C�x���g�^�OD�̕��͂ł�");

        // �\�����@���ς���Ă��镶��
        tdEvent.Add("Guooo!!!", "???,,�O�D�I�I�I�I�I�I�H�H�H�H�H�H�I�I�I�I�I");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //�Փ˂��Ă������肪Player�Ȃ�playercontroller�̃t���O�I��
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("TDEvent��OnTrigetr");
            isTDEvent = true;
        }
    }

    public void StartTDEvent(string eventTag)
    {
        // �R���C�_�[�̃^�O�ŉ�b���e���擾
        if (tdEvent.TryGetValue(eventTag, out string tdEventText))
        {
            // ��b���e��\��
            DisplayTDEvent(tdEventText);
            animateTDialog.TDialogOpen();
            callOnece = eventTag;
        }
        else
        {
            //�G���[�n���h�����O�̓��e�������ɋL�q
            Debug.Log("�g�[�N�_�C�A���O�̃^�O�����͂̎�������肭�Q�Ƃ���Ă��Ȃ����������͂�\�������Ȃ�������������");
        }
        /*if(eventTag == "TDEventTag1-A")
        {
            StartTDEvent("");
        }*/
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
        foreach (string part in parts)
        {
            Debug.Log(part);
        }
        Debug.Log(parts.Length);
    }
    

    private void Update()
    {
        //�i���[�^�[�_�C�A���O���J���Ă���ԁA���N���b�N�������ƁA���͑���
        if (animateTDialog.t_IsOpen && animateTDialog.t_SentenceTrigger == true && parts !=null && isTalkStop == false)
        {
            if (i < parts.Length - 1)
            {
                i++;
                tdEventText.text = parts[i];
                animateTDialog.t_SentenceTrigger = false;�@//�N���b�N�������Ȃ��Ă����͑��肳���̂�h��
            }
            //�������݃C�x���g
            if(callOnece == "TDEventTag1-A" && tdEventText.text == " ")
            {
                TalkChange(callOnece);
            }
            //�Ō�̕��͂ɂȂ��āA���N���b�N�������ƃt���O�I�t�AText�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            if (i >= parts.Length - 1)
            {
                Array.Clear(parts, 0, parts.Length);
                Debug.Log("�Ō�̍s");
                isTDEvent = false;
                i = 0;
                tdEvent.Remove(callOnece);
            }
        }
    }

    //�b�̍Œ��ɑ��̐l�����b�����邽�߂̂��́i���F�c�A����F�c�@���j
    private void TalkChange(string key)
    {
        if (key == "TDEventTag1-A")
        {
            StartTDEvent("Guooo!!!");
            //DOTween���g���ĉ�ʂ�h�炵����
            //�h�ꂪ���܂�܂ł��A���܂��Đ��b���܂ő҂���������R���[�`���H
        }
    }
    //��b�̊Ԃ���������֐�
    IEnumerator StopTalk(string key)
    {
        StartCoroutine(Seconds3());
        yield return new WaitForSeconds(1);
    }
    IEnumerator Seconds3()
    {
        isTalkStop = true;
        Time.timeScale = 0;
        yield return new WaitForSeconds(3);
        Time.timeScale = 1;
        isTalkStop = false;
    }
    //�b�̍Œ��ɑ��̏o�������N����ANDialog�Řb��W�J���邽�߂̂���
    //public isTalkStop��true�ŃN���b�N���Ă�TDialog���������Ȃ�����NDialog��\���ł���
    //public IEnumerator StopTalkUntill(string key)
    //{
    //    yield return new WaitUntil(() =>
    //    {
    //        if (key == "TDEventTag1-A")
    //        {
    //            isTalkStop = true;
    //            StartTDEvent("Guooo!!!");
    //            return isTalkStop == false;
    //        }
    //        else
    //        {
    //            return true;
    //        }
    //    });
    //}
}
