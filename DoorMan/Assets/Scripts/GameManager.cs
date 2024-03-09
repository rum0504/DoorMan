using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreText; // ���𐔂�\������e�L�X�gUI
    public GameObject finishPanel; // �Q�[���I�[�o�[���ɕ\������p�l��UI
    public GameObject rankingPanel;
    public Button returnToTitleButton; // �^�C�g���ɖ߂�{�^��
    public Button restartButton; // �Q�[�������X�^�[�g����{�^��
    public Button rankingButton;

    int score = 0; // ����
    bool gameIsOver; // �Q�[�����I���������ǂ����̃t���O
    bool rankingPanelisActive; //�����L���O�p�l�����\������Ă��邩�̃t���O

    public Text finalScoreText;


    void Start()
    {
        score = 0;
        int cns = PlayerPrefs.GetInt("score");
        Debug.Log(cns);
        gameIsOver = false;
        finishPanel.SetActive(false);
        rankingPanelisActive = false;
        rankingPanel.SetActive(false);
        UpdateScoreText();


    }

    public void UpdateScore(int scoreIncrement)
    {
        score += scoreIncrement; // �X�R�A���X�V
        UpdateScoreText(); // �X�R�A�\�����X�V
    }

    void UpdateScoreText()
    {
        scoreText.text = "����: " + score.ToString();
    }

    public void GameOver()
    {
        Debug.Log("GameOver!");
        gameIsOver = true;
        finishPanel.SetActive(true);
        finalScoreText.text = score.ToString() + "�␳������";
        returnToTitleButton.onClick.RemoveAllListeners(); // ���X�i�[����U�N���A
        restartButton.onClick.RemoveAllListeners(); // ���X�i�[����U�N���A
        returnToTitleButton.onClick.AddListener(ReturnToTitle);
        restartButton.onClick.AddListener(RestartGame);
        rankingButton.onClick.AddListener(GoToRanking);
        PlayerPrefs.SetInt("score", score);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void GoToRanking()
    {
        rankingPanelisActive = true;
        rankingPanel.SetActive(true);
    }
}
