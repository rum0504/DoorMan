using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreText; // ���𐔂�\������e�L�X�gUI
    public GameObject finishPanel; // �Q�[���I�[�o�[���ɕ\������p�l��UI
    public Button returnToTitleButton; // �^�C�g���ɖ߂�{�^��
    public Button restartButton; // �Q�[�������X�^�[�g����{�^��
    public Button rankingButton;

    int score = 0; // ����
    bool gameIsOver = false; // �Q�[�����I���������ǂ����̃t���O

    void Start()
    {
        finishPanel.SetActive(false);
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
        returnToTitleButton.onClick.RemoveAllListeners(); // ���X�i�[����U�N���A
        restartButton.onClick.RemoveAllListeners(); // ���X�i�[����U�N���A
        returnToTitleButton.onClick.AddListener(ReturnToTitle);
        restartButton.onClick.AddListener(RestartGame);
        rankingButton.onClick.AddListener(GoToRanking);
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
        SceneManager.LoadScene("Ranking");
    }
}
