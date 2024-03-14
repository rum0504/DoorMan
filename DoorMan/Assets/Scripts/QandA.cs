using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;

public class QandA : MonoBehaviour
{
    public GameManager gameManager;
    public RandomVisitorController randomVisitorController;
    public Text questionText;
    public Button[] answerButtons;
    public QuestionAndAnswer[] questionAndAnswers;
    public VideoPlayer effectVideoPlayer;
    public int correctAnswersForEffect = 10;
    private int correctAnswersCount = 0;
    private List<int> questionIndexes = new List<int>();
    public AudioSource correctAnswerSound;

    void Start()
    {
        ShuffleQuestionIndexes();
        ShowNextQuestion();
        effectVideoPlayer.gameObject.SetActive(false);
    }

    void ShuffleQuestionIndexes()
    {
        questionIndexes.Clear();
        for (int i = 0; i < questionAndAnswers.Length; i++)
        {
            questionIndexes.Add(i);
        }
        for (int i = 0; i < questionIndexes.Count; i++)
        {
            int temp = questionIndexes[i];
            int randomIndex = Random.Range(i, questionIndexes.Count);
            questionIndexes[i] = questionIndexes[randomIndex];
            questionIndexes[randomIndex] = temp;
        }
    }

    void CheckAnswer(string chosenAnswer, int currentQuestionIndex)
    {
        if (chosenAnswer == questionAndAnswers[currentQuestionIndex].correctAnswer)
        {
            Debug.Log("Correct!");
            if (correctAnswerSound != null)
            {
                correctAnswerSound.Play();
            }
            gameManager.UpdateScore(1);
            correctAnswersCount++;

            if (correctAnswersCount % correctAnswersForEffect == 0)
            {
                effectVideoPlayer.gameObject.SetActive(true);
                PlayEffect();
            }

            if (randomVisitorController != null)
            {
                randomVisitorController.SpawnVisitor();
            }
            else
            {
                Debug.LogError("randomVisitorController is null.");
            }

            if (correctAnswersCount % correctAnswersForEffect != 0 && questionIndexes.Count > 0)
            {
                ShowNextQuestion(); // ここで次の質問を表示
            }
        }
        else
        {
            Debug.Log("Wrong...");
            gameManager.GameOver();
        }
    }

    void ShowNextQuestion()
    {
        if (questionIndexes.Count == 0)
        {
            ShuffleQuestionIndexes();
        }
        int nextQuestionIndex = questionIndexes[0];
        questionIndexes.RemoveAt(0);

        questionText.text = questionAndAnswers[nextQuestionIndex].question;

        string[] answers = ShuffleAnswers(questionAndAnswers[nextQuestionIndex].answers);
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = answers[i];
            string chosenAnswer = answers[i];
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => CheckAnswer(chosenAnswer, nextQuestionIndex));
        }
    }



    string[] ShuffleAnswers(string[] answers)
    {
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
        if (effectVideoPlayer != null && !effectVideoPlayer.isPlaying && effectVideoPlayer.isActiveAndEnabled)
        {
            effectVideoPlayer.Play();
            effectVideoPlayer.loopPointReached += OnEffectVideoEnd;
        }
    }

    void OnEffectVideoEnd(UnityEngine.Video.VideoPlayer vp)
    {
        vp.loopPointReached -= OnEffectVideoEnd;
        effectVideoPlayer.Stop();
        effectVideoPlayer.gameObject.SetActive(false);
        correctAnswersCount = 0; // 正解数のカウントをリセットする
    }

}

[System.Serializable]
public class QuestionAndAnswer
{
    public string question;
    public string[] answers;
    public string correctAnswer;
}
