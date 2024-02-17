using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NDEvent : MonoBehaviour
{

    [SerializeField] AnimateNDialog animateNDialog;
    private string callOnece;
    public Text ndEventText;
    private string[] parts;
    int i = 0;
    public bool isNDEvent;
    ////������`
    private Dictionary<string, string> ndEvent = new Dictionary<string, string>();

    private void Start()
    {
        //��b���ǉ��B�u,,�v�ŕ��͂̋�؂�B�^�O��Unity�̕��ƍ��킹�āANDEvent��t����
        ndEvent.Add("NDEventTag1-A", "�����A����͌��̖����ł�,,�߂Â��Ɠˌ����Ă��邩�璍�ӂ��āI,,���h���ȏォ�畢�����Ԃ���Ζ�����H�ׂ邱�Ƃ��ł����");
        ndEvent.Add("NDEventTagB", "�C�x���g�^�OB�̕��͂ł�");
        ndEvent.Add("NDEventTagC", "�C�x���g�^�OC�̕��͂ł�");
        ndEvent.Add("NDEventTagD", "�C�x���g�^�OD�̕��͂ł�");

        ndEvent.Add("Title1", "SLIME RPG �ւ悤����,,���Ȃ��̓X���C���Ƃ��Ė`�����Ă��炢�܂�,,�����̉Ƃ̃h�A�ɍs���Ɩ`�����n�܂�܂�,,A�L�[�AD�L�[�ō��E�Ɉړ�\nF�L�[�������Ȃ���ړ��ő��������܂�,,Space�L�[�ŃW�����v���邱�Ƃ��o���܂�,,�ł́A�悢�`�����I");
        //Stage1����N�����̕\�����́A���s��Stage1�X�N���v�g
        ndEvent.Add("Stage1", "�����̓꒣��ɏZ�ރX���C���͕��a�ɕ�炵�Ă��܂���,,�������A���邱�Ƃœ꒣������߂�l�V���Ƀ��J���Ă��܂��܂���,,�X���C���͎l�V����|�����ɏo�܂�");
        ndEvent.Add("Skill", "������ˁI\n���̖��������ݍ��߂��ˁI,,�X���C���͈��ݍ��񂾓G�̏�����荞��ŃX�L���Ƃ��Ďg����悤�ɂȂ��,,�X�L���͉�ʂ̉��ɕ\��������,,�g�������X�L���ɃJ�[�\�������킹�ăN���b�N���Ă�");
        
    }

    public void StartNDEvent(string eventTag)
    {
        // �R���C�_�[�̃^�O�ŉ�b���e���擾
        if (ndEvent.TryGetValue(eventTag, out string ndEventText))
        {
            // ��b���e��\��
            DisplayNDEvent(ndEventText);
            animateNDialog.DialogNarratorOpen();
            callOnece = eventTag;
            Debug.Log("StartNDEvent");
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
        //�Փ˂��Ă������肪Player�Ȃ�playercontroller�̃t���O�I��
        if (collider.gameObject.CompareTag("Player"))
        {
            isNDEvent = true;
        }
    }

    private void Update()
    {
        //�i���[�^�[�_�C�A���O���J���Ă���ԁA���N���b�N�������ƁA���͑���
        if (animateNDialog.IsOpen && animateNDialog.n_SentenceTrigger == true && parts != null)
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
                Array.Clear(parts, 0, parts.Length);
                isNDEvent = false;
                i=0;
                ndEvent.Remove(callOnece);
            }
        }
    }
}
