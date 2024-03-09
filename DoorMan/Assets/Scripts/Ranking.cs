using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    [SerializeField, Header("表示させるテキスト")]
    Text[] rankingText = new Text[5];

    string[] ranking = { "ランキング1位", "ランキング2位", "ランキング3位", "ランキング4位", "ランキング5位" };
    int[] rankingValue = new int[5];

    // Use this for initialization
    void Start()
    {
        GetRanking();

        SetRanking(); // ランキングを更新するためにGameManagerからスコアを取得する

        for (int i = 0; i < rankingText.Length; i++)
        {
            rankingText[i].text = (i + 1) + "位: " + rankingValue[i].ToString();
        }
    }

    /// <summary>
    /// ランキング呼び出し
    /// </summary>
    void GetRanking()
    {
        //ランキング呼び出し
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
        }
    }

    /// <summary>
    /// ランキング書き込み
    /// </summary>
    void SetRanking()
    {
        // GameManagerからスコアを取得してランキングに追加する
        int score = PlayerPrefs.GetInt("score", 0); // GameManagerが保存したスコアを取得する
        for (int i = 0; i < ranking.Length; i++)
        {
            if (score > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = score;
                score = change;
            }
        }

        //入れ替えた値を保存
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
        }
        PlayerPrefs.Save();
    }
}
