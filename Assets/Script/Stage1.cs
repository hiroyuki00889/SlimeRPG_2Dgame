using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    [SerializeField] AnimateNDialog animateNDialog;
    [SerializeField] NDEvent ndEvent;
    void Start()
    {
        //������J��Ԃ�����f�o�b�N�p����
        PlayerPrefs.DeleteAll();
        //�����̃L�[�Ɠ���������Ŕ���
        string tag = "Stage1";
        if (!PlayerPrefs.HasKey("FirstStage1"))
        {
            //����N�����̏��������s
            ndEvent.isNDEvent = true;
            animateNDialog.DialogNarratorOpen();
            ndEvent.StartNDEvent(tag);

            //Stage1�L�[�ɒl������
            PlayerPrefs.SetInt("FirstStage1", 1);
            PlayerPrefs.Save();
            
        }
    }

}
