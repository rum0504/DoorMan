using UnityEngine;
using UnityEngine.UI;

public class QandA : MonoBehaviour
{
    public Text questionText; // ����\������e�L�X�gUI
    public Button[] answerButtons; // ������\������{�^��UI
    public QuestionAndAnswer[] questionAndAnswers; // ���Ɠ����̔z��

    public int currentQuestionIndex = -1; // ���݂̖��̃C���f�b�N�X
    public GameManager gameManager; // GameManager�ւ̎Q��

    void Start()
    {
        ShowNextQuestion();
    }

    public void ShowNextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex >= questionAndAnswers.Length)
        {
            // ��肪�I�������ꍇ�͍ŏ��̖��ɖ߂�
            currentQuestionIndex = 0;
        }

        // ���Ɠ�����\��
        questionText.text = questionAndAnswers[currentQuestionIndex].question;

        // �{�^���ɑI�����������_���ɕ\��
        string[] answers = ShuffleAnswers(questionAndAnswers[currentQuestionIndex].answers);
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = answers[i];
            string chosenAnswer = answers[i]; // �����_�����ł̐������Q�Ƃ��m�ۂ���
            answerButtons[i].onClick.RemoveAllListeners(); // ���X�i�[����U�N���A
            answerButtons[i].onClick.AddListener(() => CheckAnswer(chosenAnswer));
        }
    }

    void CheckAnswer(string chosenAnswer)
    {
        if (chosenAnswer == questionAndAnswers[currentQuestionIndex].correctAnswer)
        {
            Debug.Log("����!");
            // �����̏������L�q����
        }
        else
        {
            Debug.Log("�s����...");
            // GameManager��GameOver���Ăяo��
            gameManager.GameOver();
        }

        // ���̖���\��
        ShowNextQuestion();
    }

    string[] ShuffleAnswers(string[] answers)
    {
        // Fisher-Yates�A���S���Y���Ŕz����V���b�t��
        for (int i = answers.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            string temp = answers[i];
            answers[i] = answers[randomIndex];
            answers[randomIndex] = temp;
        }
        return answers;
    }
}

[System.Serializable]
public class QuestionAndAnswer
{
    public string question; // ���
    public string[] answers; // �I����
    public string correctAnswer; // ����������
}