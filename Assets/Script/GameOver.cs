using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // �{�^����Inspector�Ŋ֘A�t���邽�߂̕ϐ�
    public Button ButtonRestart;
    public Button ButtonGiveup;
    public string LastScene;

    void Start()
    {
        ButtonGiveup.onClick.AddListener(ToTitle);
        ButtonRestart.onClick.AddListener(ReStart);
    }

    void ToTitle()
    {
        // �w��̃V�[����ǂݍ���
        SceneManager.LoadScene("TitleScene");
    }

    void ReStart()
    {
        LifeManage lifemanage = FindObjectOfType<LifeManage>();
        string currentSceneName = lifemanage.currentSceneName;
        // �w��̃V�[����ǂݍ���
        SceneManager.LoadScene(currentSceneName);
    }
}
