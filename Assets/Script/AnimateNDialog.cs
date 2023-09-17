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
    public bool IsOpen => gameObject.activeSelf;// �_�C�A���O�͊J���Ă��邩�ǂ���
    public bool IsTransition = false;// �A�j���[�V���������ǂ���
    public bool enterTrigger = false;//�G���^�[�L�[�ŉ�b��i�߂�t���O
    public bool isNDialog = false; //NDialog�\���t���O

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�Փ˂�������̃^�O��Events���A1�x����̃t���O���I���ɂȂ��Ă��邩
        if (collision.gameObject.CompareTag("Events") && firstEvent != null && firstEvent.isFirstEvent == true
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
        //�Փ˂�������̃^�O��Events���A1�x����̊e�t���O���I���ɂȂ��Ă��邩
        if (isNDialog == true && firstEvent.isFirstEvent == true
            )
        {
            DialogNarratorOpen();

        }
        //
        if (enterTrigger == true && firstEvent.isFirstEvent == false) 
        {
            DialogNarratorClose();
            enterTrigger = false;
            isNDialog= false;
        }
    }

    //private void DialogNarratorOpen()
    public void DialogNarratorOpen()

    {
        if (IsOpen || IsTransition) return; // �s������h�~
        gameObject.SetActive(true); // DialogNarrator���̂��A�N�e�B�u�ɂ���
        m_Animator.SetBool(ParamIsOpen, true); // IsOpen�t���O��true�ɃZ�b�g
        // �A�j���[�V�����ҋ@
        StartCoroutine(WaitAnimation("Shown"));
    }

    private void DialogNarratorClose()
    {   
        if (!IsOpen || IsTransition) return;
        m_Animator.SetBool(ParamIsOpen, false); // IsOpen�t���O��false�ɃZ�b�g
        // �A�j���[�V�����ҋ@���A�I�������p�l�����̂��A�N�e�B�u�ɂ���
        StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));
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
