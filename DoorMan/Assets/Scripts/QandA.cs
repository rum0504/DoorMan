using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QandA : MonoBehaviour
{
    public GameManager gameManager; // GameManager�ւ̎Q�Ƃ�ێ�����ϐ�
    public RandomVisitorController randomVisitorController;
    public Text questionText; // ����\������e�L�X�gUI
    public Button[] answerButtons; // ������\������{�^��UI
    public QuestionAndAnswer[] questionAndAnswers; // ���Ɠ����̔z��

    int currentQuestionIndex = -1; // ���݂̖��̃C���f�b�N�X

    void Start()
    {
        randomVisitorController = GameObject.FindObjectOfType<RandomVisitorController>();
        ShowNextQuestion();
    }

    public void ShowNextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex >= questionAndAnswers.Length)
        {
            // ��肪�I�������ꍇ�͍ŏ��̖��ɖ߂�̂ł͂Ȃ��A�Ō�̖��ɒB�����ꍇ�ɏ��������Ȃ�
            currentQuestionIndex = questionAndAnswers.Length - 1;
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
            gameManager.UpdateScore(1); // GameManager��UpdateScore�֐����Ăяo���ăX�R�A�𑝂₷

            // ���������痈�K�҂��������ĐV�������K�҂𐶐�����
            GameObject visitor = GameObject.FindGameObjectWithTag("Visitor");
            if (visitor != null)
            {
                Destroy(visitor);
                randomVisitorController.SpawnVisitor(); // �V�������K�҂𐶐�
            }
            else
            {
                Debug.Log("���K�҂����łɔj������Ă��܂��B");
            }
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
