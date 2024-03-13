using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;

public class QandA : MonoBehaviour
{
    public GameManager gameManager; // GameManagerへの参照を保持する変数
    public RandomVisitorController randomVisitorController;
    public Text questionText; // 問題を表示するテキストUI
    public Button[] answerButtons; // 答えを表示するボタンUI
    public QuestionAndAnswer[] questionAndAnswers; // 問題と答えの配列
    public VideoPlayer effectVideoPlayer; // エフェクト用のVideoPlayer
    public int correctAnswersForEffect = 10; // エフェクトを再生するために必要な正解数
    private int correctAnswersCount = 0; // 現在の正解数

    private List<int> questionIndexes = new List<int>(); // 問題のインデックスをランダムに管理するリスト
    public AudioSource correctAnswerSound;

    void Start()
    {
        // 問題のインデックスをランダムにシャッフル
        ShuffleQuestionIndexes();
        ShowNextQuestion();
    }

    void ShuffleQuestionIndexes()
    {
        // 問題のインデックスを初期化
        questionIndexes.Clear();
        // ランダムにインデックスを追加
        for (int i = 0; i < questionAndAnswers.Length; i++)
        {
            questionIndexes.Add(i);
        }
        // リストをシャッフル
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
        // 問題のインデックスを取得し、リストから削除
        if (questionIndexes.Count == 0)
        {
            // 問題がすべて表示されたらリストを再度シャッフル
            ShuffleQuestionIndexes();
        }
        int nextQuestionIndex = questionIndexes[0];
        questionIndexes.RemoveAt(0);

        // 問題と答えを表示
        questionText.text = questionAndAnswers[nextQuestionIndex].question;

        // ボタンに選択肢をランダムに表示
        string[] answers = ShuffleAnswers(questionAndAnswers[nextQuestionIndex].answers);
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = answers[i];
            string chosenAnswer = answers[i]; // ラムダ式内での正しい参照を確保する
            answerButtons[i].onClick.RemoveAllListeners(); // リスナーを一旦クリア
            answerButtons[i].onClick.AddListener(() => CheckAnswer(chosenAnswer, nextQuestionIndex));
        }
    }

    void CheckAnswer(string chosenAnswer, int currentQuestionIndex)
    {
        if (chosenAnswer == questionAndAnswers[currentQuestionIndex].correctAnswer)
        {
            Debug.Log("正解!");
            // 正解の音を再生する
            if (correctAnswerSound != null)
            {
                correctAnswerSound.Play();
            }
            // 正解の処理を記述する
            gameManager.UpdateScore(1); // GameManagerのUpdateScore関数を呼び出してスコアを増やす
            correctAnswersCount++; // 正解数を増やす

            // 正解数がエフェクトを再生するための数に達したかどうかをチェック
            if (correctAnswersCount % correctAnswersForEffect == 0)
            {
                PlayEffect(); // エフェクトを再生
            }

            // randomVisitorControllerがnullでないことを確認し、それからSpawnVisitorを呼び出す
            if (randomVisitorController != null)
            {
                randomVisitorController.SpawnVisitor(); // 新しい来訪者を生成
            }
            else
            {
                Debug.LogError("randomVisitorControllerがnullです。");
            }
        }
        else
        {
            Debug.Log("不正解...");
            // GameManagerにGameOverを呼び出す
            gameManager.GameOver();
        }

        // 次の問題を表示
        ShowNextQuestion();
    }

    string[] ShuffleAnswers(string[] answers)
    {
        // Fisher-Yatesアルゴリズムで配列をシャッフル
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
            effectVideoPlayer.Play(); // エフェクトを再生
        }
    }
}

[System.Serializable]
public class QuestionAndAnswer
{
    public string question; // 問題
    public string[] answers; // 選択肢
    public string correctAnswer; // 正しい答え
}
