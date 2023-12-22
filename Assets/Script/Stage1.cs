using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    [SerializeField] AnimateNDialog animateNDialog;
    [SerializeField] NDEvent ndEvent;
    //�����̃L�[�Ɠ���������Ŕ���
    
    string sd = "Skill";
    void Start()
    {
        //������J��Ԃ�����f�o�b�N�p����
        PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("FirstStage1"))
        {
            string tag = "Stage1";
            //����N�����̏��������s
            ndEvent.isNDEvent = true;
            animateNDialog.DialogNarratorOpen();
            ndEvent.StartNDEvent(tag);

            //Stage1�L�[�ɒl������
            PlayerPrefs.SetInt("FirstStage1", 1);
            PlayerPrefs.Save();
            
        }
    }
    //�ŏ��̌���|�����Ƃ��ɃX�L���Ȃǂ̐����_�C�A���O
    void OnDisable()
    {
        if (!PlayerPrefs.HasKey("SkillDiscribe"))
        {
            //�X�L������
            ndEvent.isNDEvent = true;
            animateNDialog.DialogNarratorOpen();
            ndEvent.StartNDEvent(sd);

            //Stage1�L�[�ɒl������
            PlayerPrefs.SetInt("Skilldiscribe", 1);
            PlayerPrefs.Save();
        }
        //���𖳎����čs�����Ƃ���v���C���[�ɑ΂��鏈�����������ɏ���
        Debug.Log("OnDisable");
    }
}
