using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using Unity.VisualScripting;

public class BearScrollEvent : MonoBehaviour
{
    public GameObject parent;
    public Vector3 scrollspeed= new Vector3(0.01f, 0, 0); //�X�N���[���X�s�[�h
    public Vector3 upspeed; //�X�s�[�h�A�b�v���̑��x
    private bool a;
    private Animator bearanim;
    public GameObject main_camera;
    private GameObject parentob;
    public GameObject chase_camera;

    private int roat = Animator.StringToHash("Roat");
    private int stay = Animator.StringToHash("Stay");
    private int bearbreak = Animator.StringToHash("Break");

    [SerializeField] private float[] eventcoordinate; //�C�x���g�̍��W
    private int i = 0;
    [SerializeField] private UnityEvent[] events; //�C�x���g�̍��W�ɓ��B���������̃C�x���g���N������

    private bool staynow;

    private void Start()
    {
        bearanim = GetComponent<Animator>();
        main_camera = GameObject.Find("Main_Camera");
        parentob = GameObject.Find("Camera,BackGrounds");
        chase_camera = parentob.transform.Find("Chase_Camera").gameObject;
    }

    private void Update() 
    {
        if (!staynow) 
        {
            parent.transform.position -= scrollspeed; //���݂͐e�I�u�W�F�N�g�̍��W����(�F���̂��̂��ƃ��[���h���W�ɕϊ����鏈�������肻���Ȃ���)

            if (parent.transform.position.x < eventcoordinate[i]) 
            {
                events[i].Invoke();
                i++;
            }
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    main_camera.SetActive(!main_camera.activeSelf);
        //    chase_camera.SetActive(!chase_camera.activeSelf);
        //}
    }


    public void SpeedUp() 
    {
        scrollspeed*=1.5f;
    }

    public void BearSpeedUp() //�X�s�[�h�_�E����K�������Ȃ�A�����ŕb���w�肵�ČĂяo��
    {
        this.gameObject.transform.position += scrollspeed;
    }

    public void BearSpeedDown()
    {
        this.gameObject.transform.position -= scrollspeed;
    }

    public void BearStay(float time) //�w�肳�ꂽ�b���҂�
    {
        StartCoroutine(BearStayTime(time));
    }

    public void BearRoat()
    {
        bearanim.SetTrigger(roat);
        BearStay(2f); //�㌄�@�G���W�܂��Ă���܂ł̎��Ԃ��w��
    }

    public void BearBreak()
    {
        bearanim.SetTrigger(bearbreak);
        //�G�t�F�N�g�┻��Ȃ�
    }

    private IEnumerator BearStayTime(float time)
    {
        bearanim.SetBool(stay, true);
        staynow = true;
        yield return new WaitForSeconds(time);
        staynow = false;
        bearanim.SetBool(stay, false);
    }

    public void DiagonalUp(float time) 
    {
        StartCoroutine(Diagonal(time,true));
    }

    public void DiagonalDown(float time)
    {
        StartCoroutine(Diagonal(time, false));
    }

    private IEnumerator Diagonal(float time,bool b)//bool�l�͏オ�邩�����邩 true�ŏオ��
    {
        if (b)
        {
            scrollspeed.y = 0.01f; //�ǂꂭ�炢�オ�邩(�����邩)
        }
        else 
        {
            scrollspeed.y = -0.01f;
        }
        yield return new WaitForSeconds(time);
        scrollspeed.y = 0;
    }

    //���ꂽ���̏���
}