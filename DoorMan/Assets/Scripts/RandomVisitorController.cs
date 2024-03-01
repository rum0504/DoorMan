using UnityEngine;
using System.Collections;

public class RandomVisitorController : MonoBehaviour
{
    public GameObject[] visitorPrefabs; // インスペクターからアタッチされた来訪者のプレハブ
    public float spawnInterval = 3f; // 来訪者の生成間隔
    public Vector3 doorManPosition; // ドアマンの座標

    private Coroutine spawnCoroutine; // 来訪者の生成コルーチンの参照

    private void Start()
    {
        // インターバルごとに来訪者を生成するコルーチンを開始
        spawnCoroutine = StartCoroutine(SpawnVisitors());
    }

    private void OnDestroy()
    {
        // オブジェクトが破棄されるときにコルーチンも停止する
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    public IEnumerator SpawnVisitors()
    {
        while (true)
        {
            // プレハブからランダムに来訪者を選択
            GameObject randomVisitorPrefab = visitorPrefabs[Random.Range(0, visitorPrefabs.Length)];

            // プレハブから来訪者を生成
            GameObject newVisitor = Instantiate(randomVisitorPrefab, transform.position, Quaternion.identity);

            // ドアマンの方向に向かって歩かせる
            MoveTowardsDoorMan(newVisitor.transform);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void MoveTowardsDoorMan(Transform visitorTransform)
    {
        StartCoroutine(MoveVisitor(visitorTransform, doorManPosition));
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

        // 目的地に到達したらオブジェクトを破棄する
        Destroy(visitorTransform.gameObject);
    }

    // 新しい来訪者を生成するメソッド
    public void SpawnNewVisitor()
    {
        GameObject randomVisitorPrefab = visitorPrefabs[Random.Range(0, visitorPrefabs.Length)];
        GameObject newVisitor = Instantiate(randomVisitorPrefab, transform.position, Quaternion.identity);
        MoveTowardsDoorMan(newVisitor.transform);
    }
}
