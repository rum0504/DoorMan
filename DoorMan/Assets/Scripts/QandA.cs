using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QandA : MonoBehaviour
{
    public GameManager gameManager; // GameManagerへの参照を保持する変数
    public RandomVisitorController randomVisitorController;
    public Text questionText; // 問題を表示するテキストUI
    public Button[] answerButtons; // 答えを表示するボタンUI
    public QuestionAndAnswer[] questionAndAnswers; // 問題と答えの配列

    int currentQuestionIndex = -1; // 現在の問題のインデックス

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
            // 問題が終了した場合は最初の問題に戻るのではなく、最後の問題に達した場合に初期化しない
            currentQuestionIndex = questionAndAnswers.Length - 1;
        }

        // 問題と答えを表示
        questionText.text = questionAndAnswers[currentQuestionIndex].question;

        // ボタンに選択肢をランダムに表示
        string[] answers = ShuffleAnswers(questionAndAnswers[currentQuestionIndex].answers);
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = answers[i];
            string chosenAnswer = answers[i]; // ラムダ式内での正しい参照を確保する
            answerButtons[i].onClick.RemoveAllListeners(); // リスナーを一旦クリア
            answerButtons[i].onClick.AddListener(() => CheckAnswer(chosenAnswer));
        }
    }


    void CheckAnswer(string chosenAnswer)
    {
        if (chosenAnswer == questionAndAnswers[currentQuestionIndex].correctAnswer)
        {
            Debug.Log("正解!");
            // 正解の処理を記述する
            gameManager.UpdateScore(1); // GameManagerのUpdateScore関数を呼び出してスコアを増やす

            // 正解したら来訪者を消去して新しい来訪者を生成する
            GameObject visitor = GameObject.FindGameObjectWithTag("Visitor");
            if (visitor != null)
            {
                Destroy(visitor);
                randomVisitorController.SpawnVisitor(); // 新しい来訪者を生成
            }
            else
            {
                Debug.Log("来訪者がすでに破棄されています。");
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
}

[System.Serializable]
public class QuestionAndAnswer
{
    public string question; // 問題
    public string[] answers; // 選択肢
    public string correctAnswer; // 正しい答え
}
