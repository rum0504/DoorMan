using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreText; // 正解数を表示するテキストUI
    public GameObject finishPanel; // ゲームオーバー時に表示するパネルUI
    public Button returnToTitleButton; // タイトルに戻るボタン
    public Button restartButton; // ゲームをリスタートするボタン
    public Button rankingButton;

    int score = 0; // 正解数
    bool gameIsOver = false; // ゲームが終了したかどうかのフラグ

    void Start()
    {
        finishPanel.SetActive(false);
        UpdateScoreText();
    }

    public void UpdateScore(int scoreIncrement)
    {
        score += scoreIncrement; // スコアを更新
        UpdateScoreText(); // スコア表示を更新
    }

    void UpdateScoreText()
    {
        scoreText.text = "正解数: " + score.ToString();
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
