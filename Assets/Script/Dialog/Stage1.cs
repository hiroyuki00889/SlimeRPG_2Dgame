using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    [SerializeField] AnimateNDialog animateNDialog;
    [SerializeField] NDEvent ndEvent;
    //�����̃L�[�Ɠ���������Ŕ���
    void Start()
    {
            string tag = "Stage1";
            //����N�����̏��������s
            ndEvent.isNDEvent = true;
            ndEvent.StartNDEvent(tag);

    }
    //�ŏ��̌���|�����Ƃ��ɃX�L���Ȃǂ̐����_�C�A���O
    
}
