using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] AnimateNDialog animateNDialog;
    [SerializeField] NDEvent ndEvent;
    private string title = "Title1";
    void Start()
    {
        //������J��Ԃ�����f�o�b�N�p����
        PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("Title"))
        {
            Debug.Log("�n�X�L�[");
            //����N�����̏��������s
            ndEvent.isNDEvent = true;
            animateNDialog.DialogNarratorOpen();
            ndEvent.StartNDEvent(title);

            //Stage1�L�[�ɒl������
            PlayerPrefs.SetInt("Title", 1);
            PlayerPrefs.Save();

        }
    }
}
