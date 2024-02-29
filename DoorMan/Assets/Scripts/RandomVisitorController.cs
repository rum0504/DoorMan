using UnityEngine;
using System.Collections;

public class RandomVisitorController : MonoBehaviour
{
    public GameObject[] visitorPrefabs; // インスペクターからアタッチされた来訪者のプレハブ
    public float spawnInterval = 3f; // 来訪者の生成間隔
    public Vector3 doorManPosition; // ドアマンの座標

    private GameManager gameManager; // GameManagerへの参照を保持

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // GameManagerを探して参照を取得
    }

    public void SpawnVisitors()
    {
        StartCoroutine(SpawnVisitorCoroutine());
    }

    private IEnumerator SpawnVisitorCoroutine()
    {
        // プレハブからランダムに来訪者を選択
        GameObject randomVisitorPrefab = visitorPrefabs[Random.Range(0, visitorPrefabs.Length)];

        // プレハブから来訪者を生成
        GameObject newVisitor = Instantiate(randomVisitorPrefab, new Vector3(0, 0.5f, 10), Quaternion.identity);

        // ドアマンの方向に向かって歩かせる
        MoveTowardsDoorMan(newVisitor.transform);

        yield return new WaitForSeconds(spawnInterval);
    }

    private void MoveTowardsDoorMan(Transform visitorTransform)
    {
        // ドアマンの方向に向かって歩かせる
        Vector3 targetPosition = doorManPosition;
        targetPosition.y = visitorTransform.position.y; // ドアマンと同じ高さに合わせる
        visitorTransform.LookAt(targetPosition); // ドアマンの方向を向く
        StartCoroutine(MoveVisitor(visitorTransform, targetPosition));
    }

    private IEnumerator MoveVisitor(Transform visitorTransform, Vector3 targetPosition)
    {
        float moveSpeed = 3f; // 来訪者の移動速度
        while (Vector3.Distance(visitorTransform.position, targetPosition) > 0.1f)
        {
            // ドアマンの方向に移動
            visitorTransform.position = Vector3.MoveTowards(visitorTransform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
