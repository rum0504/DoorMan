using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject randomVisitorControllerObject; // RandomVisitorController�I�u�W�F�N�g
    public QandA qAndAScript; // QandA�X�N���v�g
    public Text scoreText; // ���𐔂�\������e�L�X�gUI
    public Text timerText; // �c�莞�Ԃ�\������e�L�X�gUI
    public GameObject finishPanel; // �Q�[���I�[�o�[���ɕ\������p�l��UI
    public Button returnToTitleButton; // �^�C�g���ɖ߂�{�^��
    public Button restartButton; // �Q�[�������X�^�[�g����{�^��
    public float timeLimitPerQuestion = 5f; // 1�₠����̐�������

    int score = 0; // ����
    float remainingTime; // �c�莞��
    bool gameIsOver = false; // �Q�[�����I���������ǂ����̃t���O

    void Start()
    {
        finishPanel.SetActive(false);
        remainingTime = timeLimitPerQuestion; // �c�莞�Ԃ�ݒ�
        StartCoroutine(SpawnVisitorCoroutine());
        UpdateScoreText();
    }

    void Update()
    {
        if (!gameIsOver)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerText();

            if (remainingTime <= 0)
            {
                GameOver();
            }
        }
    }

    IEnumerator SpawnVisitorCoroutine()
    {
        while (!gameIsOver)
        {
            // �����_���ȗ��K�҂̐���
            randomVisitorControllerObject.GetComponent<RandomVisitorController>().SpawnVisitors();

            // ��莞�ԑҋ@
            yield return new WaitForSeconds(timeLimitPerQuestion);

            // ���Ԑ؂�Ȃ�
            if (remainingTime <= 0)
            {
                GameOver();
            }
        }
    }

    public void CheckAnswer(string chosenAnswer)
    {
        if (chosenAnswer == qAndAScript.questionAndAnswers[qAndAScript.currentQuestionIndex].correctAnswer)
        {
            // �����̏���
            score++;
            UpdateScoreText();
            // ���̖���
            qAndAScript.ShowNextQuestion();
            // �������Ԃ����Z�b�g
            remainingTime = timeLimitPerQuestion;
        }
        else
        {
            GameOver();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void UpdateTimerText()
    {
        if (remainingTime == Mathf.Infinity)
        {
            timerText.text = "Time: ��";
        }
        else
        {
            timerText.text = "Time: " + Mathf.RoundToInt(remainingTime).ToString();
        }
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
