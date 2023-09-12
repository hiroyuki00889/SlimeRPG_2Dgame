using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimateNDialog : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private int layer;
    [SerializeField] FirstEvent firstEvent;
    //[SerializeField] FirstEnemy firstEnemy;
    // IsOpen�t���O(�A�j���[�^�[�R���g���[���[���Œ�`�����t���O)
    private static readonly int ParamIsOpen = Animator.StringToHash("IsOpen");
    // �_�C�A���O�͊J���Ă��邩�ǂ���
    public bool IsOpen => gameObject.activeSelf;//= false;
    // �A�j���[�V���������ǂ���
    public bool IsTransition { get; private set; }
    //�G���^�[�L�[�ŉ�b��i�߂�t���O
    public bool enterTrigger = false;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        Debug.Log(IsOpen);
        Debug.Log(gameObject.activeSelf);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�Փ˂�������̃^�O��Events���A1�x����̃t���O���I���ɂȂ��Ă��邩
        if (collision.gameObject.CompareTag("Events") && firstEvent.isFirstEvent == true
            )
        {
            DialogNarratorOpen();
        }
    }
    private void Update()
    {
        //�i���[�^�[�_�C�A���O���A�N�e�B�u���A�G���^�[�L�[�Ńi���[�^�[�̕��͂�i�߂�g���K�[�ݒ�
        if (Input.GetKeyDown(KeyCode.Return) && gameObject.activeSelf == true)
        {
            enterTrigger = true;
        }
        //
        if(enterTrigger == true && firstEvent.isFirstEvent == false) 
        {
            DialogNarratorClose();
            enterTrigger = false;
        }
    }

    private void DialogNarratorOpen()
    {
        // �s������h�~
        if (IsOpen || IsTransition) return;
        // DialogNarrator���̂��A�N�e�B�u�ɂ���
        gameObject.SetActive(true);//IsOpen = true;
        // IsOpen�t���O��true�ɃZ�b�g
        m_Animator.SetBool(ParamIsOpen, true);
        // �A�j���[�V�����ҋ@
        StartCoroutine(WaitAnimation("Shown"));
    }

    private void DialogNarratorClose()
    {   
        if (!IsOpen || IsTransition) return;
        // IsOpen�t���O��false�ɃZ�b�g
        m_Animator.SetBool(ParamIsOpen, false);
        // �A�j���[�V�����ҋ@���A�I�������p�l�����̂��A�N�e�B�u�ɂ���
        StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));//IsOpen = false;
    }

    private IEnumerator WaitAnimation(string stateName, UnityAction onCompleted = null)
    {
        //���̃u�[���l��true�̊Ԃ͏�2�̊֐��������Ȃ�
        IsTransition = true;

        yield return new WaitUntil(() =>
        {
              //�X�e�[�g���ω����A�A�j���[�V�������I������܂őҋ@
              var state = m_Animator.GetCurrentAnimatorStateInfo(layer);
              return state.IsName(stateName) && state.normalizedTime >= 1;
        });

        IsTransition = false;
        onCompleted?.Invoke();
    }
}
