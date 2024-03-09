using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    [SerializeField, Header("�\��������e�L�X�g")]
    Text[] rankingText = new Text[5];

    string[] ranking = { "�����L���O1��", "�����L���O2��", "�����L���O3��", "�����L���O4��", "�����L���O5��" };
    int[] rankingValue = new int[5];

    // Use this for initialization
    void Start()
    {
        GetRanking();

        SetRanking(); // �����L���O���X�V���邽�߂�GameManager����X�R�A���擾����

        for (int i = 0; i < rankingText.Length; i++)
        {
            rankingText[i].text = (i + 1) + "��: " + rankingValue[i].ToString();
        }
    }

    /// <summary>
    /// �����L���O�Ăяo��
    /// </summary>
    void GetRanking()
    {
        //�����L���O�Ăяo��
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
        }
    }

    /// <summary>
    /// �����L���O��������
    /// </summary>
    void SetRanking()
    {
        // GameManager����X�R�A���擾���ă����L���O�ɒǉ�����
        int score = PlayerPrefs.GetInt("score", 0); // GameManager���ۑ������X�R�A���擾����
        for (int i = 0; i < ranking.Length; i++)
        {
            if (score > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = score;
                score = change;
            }
        }

        //����ւ����l��ۑ�
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
        }
        PlayerPrefs.Save();
    }
}
