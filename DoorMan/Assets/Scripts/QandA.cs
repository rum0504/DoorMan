using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;

public class QandA : MonoBehaviour
{
    public GameManager gameManager; // GameManager�ւ̎Q�Ƃ�ێ�����ϐ�
    public RandomVisitorController randomVisitorController;
    public Text questionText; // ����\������e�L�X�gUI
    public Button[] answerButtons; // ������\������{�^��UI
    public QuestionAndAnswer[] questionAndAnswers; // ���Ɠ����̔z��
    public VideoPlayer effectVideoPlayer; // �G�t�F�N�g�p��VideoPlayer
    public int correctAnswersForEffect = 10; // �G�t�F�N�g���Đ����邽�߂ɕK�v�Ȑ���
    private int correctAnswersCount = 0; // ���݂̐���

    private List<int> questionIndexes = new List<int>(); // ���̃C���f�b�N�X�������_���ɊǗ����郊�X�g
    public AudioSource correctAnswerSound;

    void Start()
    {
        // ���̃C���f�b�N�X�������_���ɃV���b�t��
        ShuffleQuestionIndexes();
        ShowNextQuestion();
    }

    void ShuffleQuestionIndexes()
    {
        // ���̃C���f�b�N�X��������
        questionIndexes.Clear();
        // �����_���ɃC���f�b�N�X��ǉ�
        for (int i = 0; i < questionAndAnswers.Length; i++)
        {
            questionIndexes.Add(i);
        }
        // ���X�g���V���b�t��
        for (int i = 0; i < questionIndexes.Count; i++)
        {
            int temp = questionIndexes[i];
            int randomIndex = Random.Range(i, questionIndexes.Count);
            questionIndexes[i] = questionIndexes[randomIndex];
            questionIndexes[randomIndex] = temp;
        }
    }

    public void ShowNextQuestion()
    {
        // ���̃C���f�b�N�X���擾���A���X�g����폜
        if (questionIndexes.Count == 0)
        {
            // ��肪���ׂĕ\�����ꂽ�烊�X�g���ēx�V���b�t��
            ShuffleQuestionIndexes();
        }
        int nextQuestionIndex = questionIndexes[0];
        questionIndexes.RemoveAt(0);

        // ���Ɠ�����\��
        questionText.text = questionAndAnswers[nextQuestionIndex].question;

        // �{�^���ɑI�����������_���ɕ\��
        string[] answers = ShuffleAnswers(questionAndAnswers[nextQuestionIndex].answers);
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = answers[i];
            string chosenAnswer = answers[i]; // �����_�����ł̐������Q�Ƃ��m�ۂ���
            answerButtons[i].onClick.RemoveAllListeners(); // ���X�i�[����U�N���A
            answerButtons[i].onClick.AddListener(() => CheckAnswer(chosenAnswer, nextQuestionIndex));
        }
    }

    void CheckAnswer(string chosenAnswer, int currentQuestionIndex)
    {
        if (chosenAnswer == questionAndAnswers[currentQuestionIndex].correctAnswer)
        {
            Debug.Log("����!");
            // �����̉����Đ�����
            if (correctAnswerSound != null)
            {
                correctAnswerSound.Play();
            }
            // �����̏������L�q����
            gameManager.UpdateScore(1); // GameManager��UpdateScore�֐����Ăяo���ăX�R�A�𑝂₷
            correctAnswersCount++; // ���𐔂𑝂₷

            // ���𐔂��G�t�F�N�g���Đ����邽�߂̐��ɒB�������ǂ������`�F�b�N
            if (correctAnswersCount % correctAnswersForEffect == 0)
            {
                PlayEffect(); // �G�t�F�N�g���Đ�
            }

            // randomVisitorController��null�łȂ����Ƃ��m�F���A���ꂩ��SpawnVisitor���Ăяo��
            if (randomVisitorController != null)
            {
                randomVisitorController.SpawnVisitor(); // �V�������K�҂𐶐�
            }
            else
            {
                Debug.LogError("randomVisitorController��null�ł��B");
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

    void PlayEffect()
    {
        if (effectVideoPlayer != null)
        {
            effectVideoPlayer.Play(); // �G�t�F�N�g���Đ�
        }
    }
}

[System.Serializable]
public class QuestionAndAnswer
{
    public string question; // ���
    public string[] answers; // �I����
    public string correctAnswer; // ����������
}
