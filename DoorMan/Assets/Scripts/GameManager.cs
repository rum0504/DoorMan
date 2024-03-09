using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreText; // 正解数を表示するテキストUI
    public GameObject finishPanel; // ゲームオーバー時に表示するパネルUI
    public GameObject rankingPanel;
    public Button returnToTitleButton; // タイトルに戻るボタン
    public Button restartButton; // ゲームをリスタートするボタン
    public Button rankingButton;

    int score = 0; // 正解数
    bool gameIsOver; // ゲームが終了したかどうかのフラグ
    bool rankingPanelisActive; //ランキングパネルが表示されているかのフラグ

    public Text finalScoreText;


    void Start()
    {
        score = 0;
        int cns = PlayerPrefs.GetInt("score");
        Debug.Log(cns);
        gameIsOver = false;
        finishPanel.SetActive(false);
        rankingPanelisActive = false;
        rankingPanel.SetActive(false);
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
        finalScoreText.text = score.ToString() + "問正解した";
        returnToTitleButton.onClick.RemoveAllListeners(); // リスナーを一旦クリア
        restartButton.onClick.RemoveAllListeners(); // リスナーを一旦クリア
        returnToTitleButton.onClick.AddListener(ReturnToTitle);
        restartButton.onClick.AddListener(RestartGame);
        rankingButton.onClick.AddListener(GoToRanking);
        PlayerPrefs.SetInt("score", score);
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
        rankingPanelisActive = true;
        rankingPanel.SetActive(true);
    }
}
