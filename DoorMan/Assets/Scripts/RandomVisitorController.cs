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

            // 来訪者がドアマンの位置に着いたらゲームオーバーにする
            if (visitor.transform.position == doorManPosition)
            {
                Destroy(visitor);
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
            Destroy(oldVisitor);
        }

        // 新しい来訪者の生成
        int randomIndex = Random.Range(0, visitorPrefabs.Length);
        GameObject newVisitor = Instantiate(visitorPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        // 来訪者のRotationをy=180に固定する
        newVisitor.transform.rotation = Quaternion.Euler(0, 180, 0);
    }


}