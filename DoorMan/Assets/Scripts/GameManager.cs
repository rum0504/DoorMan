using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject randomVisitorControllerObject; // RandomVisitorControllerオブジェクト
    public QandA qAndAScript; // QandAスクリプト
    public Text scoreText; // 正解数を表示するテキストUI
    public Text timerText; // 残り時間を表示するテキストUI
    public GameObject finishPanel; // ゲームオーバー時に表示するパネルUI
    public Button returnToTitleButton; // タイトルに戻るボタン
    public Button restartButton; // ゲームをリスタートするボタン
    public float timeLimitPerQuestion = 5f; // 1問あたりの制限時間

    int score = 0; // 正解数
    float remainingTime; // 残り時間
    bool gameIsOver = false; // ゲームが終了したかどうかのフラグ

    void Start()
    {
        finishPanel.SetActive(false);
        remainingTime = timeLimitPerQuestion; // 残り時間を設定
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
            // ランダムな来訪者の生成
            randomVisitorControllerObject.GetComponent<RandomVisitorController>().SpawnVisitors();

            // 一定時間待機
            yield return new WaitForSeconds(timeLimitPerQuestion);

            // 時間切れなら
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
            // 正解の処理
            score++;
            UpdateScoreText();
            // 次の問題へ
            qAndAScript.ShowNextQuestion();
            // 制限時間をリセット
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
            timerText.text = "Time: ∞";
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
        returnToTitleButton.onClick.RemoveAllListeners(); // リスナーを一旦クリア
        restartButton.onClick.RemoveAllListeners(); // リスナーを一旦クリア
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
