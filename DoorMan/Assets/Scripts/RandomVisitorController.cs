using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomVisitorController : MonoBehaviour
{
    public GameObject[] visitorPrefabs; // 来訪者のプレハブ（複数）
    public Vector3 spawnPosition = new Vector3(0, -1.5f, 5); // 来訪者の生成位置
    public Vector3 doorManPosition = new Vector3(0, -1.5f, -7); // ドアマンの位置
    public Vector3 damagePosition = new Vector3(0, -1.5f, -5); //ダメージが入る位置
    public float speed = 1.0f; // 来訪者の移動速度

    public GameManager gameManager; // GameManagerへの参照を保持する変数
    GameObject currentVisitor; // 現在の来訪者を保持する変数

    public Image redImage;


    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        SpawnVisitor();
    }

    void Update()
    {
        GameObject visitor = GameObject.FindGameObjectWithTag("Visitor");
        if (visitor != null)
        {
            // 来訪者がドアマンの位置に向かって移動
            float step = speed * Time.deltaTime;
            visitor.transform.position = Vector3.MoveTowards(visitor.transform.position, doorManPosition, step);

            if(visitor.transform.position == damagePosition)
            {
                StartCoroutine(ColorCoroutine()); // 赤く点滅させるCoroutineを開始
            }

            // 来訪者がドアマンの位置に着いたらゲームオーバーにする
            if (visitor.transform.position == doorManPosition)
            {
                StopCoroutine(ColorCoroutine());
                gameManager.GameOver(); // GameManagerのGameOver関数を呼び出す
            }
        }
    }

    public void SpawnVisitor()
    {
        // 来訪者が存在する場合は削除
        GameObject oldVisitor = GameObject.FindGameObjectWithTag("Visitor");
        if (oldVisitor != null)
        {
            StopCoroutine(ColorCoroutine());
            Destroy(oldVisitor);
        }

        // 新しい来訪者の生成
        int randomIndex = Random.Range(0, visitorPrefabs.Length);
        GameObject newVisitor = Instantiate(visitorPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        // 来訪者のRotationをy=180に固定する
        newVisitor.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    IEnumerator ColorCoroutine()
    {
        while (true)
        {
            // 赤く点滅させる処理
            Color redColor = Color.red;
            redColor.a = 0.5f; // 透明度を設定する（0.0から1.0の間で指定）
            redImage.color = redColor;
            yield return new WaitForSeconds(0.1f);

            Color whiteColor = Color.white;
            whiteColor.a = 0f; // 透明度を設定する（0.0から1.0の間で指定）
            redImage.color = whiteColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

}
