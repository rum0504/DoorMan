using UnityEngine;
using System.Collections;

public class RandomVisitorController : MonoBehaviour
{
    public GameObject[] visitorPrefabs; // �C���X�y�N�^�[����A�^�b�`���ꂽ���K�҂̃v���n�u
    public float spawnInterval = 3f; // ���K�҂̐����Ԋu
    public Vector3 doorManPosition; // �h�A�}���̍��W

    private GameManager gameManager; // GameManager�ւ̎Q�Ƃ�ێ�

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // GameManager��T���ĎQ�Ƃ��擾
    }

    public void SpawnVisitors()
    {
        StartCoroutine(SpawnVisitorCoroutine());
    }

    private IEnumerator SpawnVisitorCoroutine()
    {
        // �v���n�u���烉���_���ɗ��K�҂�I��
        GameObject randomVisitorPrefab = visitorPrefabs[Random.Range(0, visitorPrefabs.Length)];

        // �v���n�u���痈�K�҂𐶐�
        GameObject newVisitor = Instantiate(randomVisitorPrefab, new Vector3(0, 0.5f, 10), Quaternion.identity);

        // �h�A�}���̕����Ɍ������ĕ�������
        MoveTowardsDoorMan(newVisitor.transform);

        yield return new WaitForSeconds(spawnInterval);
    }

    private void MoveTowardsDoorMan(Transform visitorTransform)
    {
        // �h�A�}���̕����Ɍ������ĕ�������
        Vector3 targetPosition = doorManPosition;
        targetPosition.y = visitorTransform.position.y; // �h�A�}���Ɠ��������ɍ��킹��
        visitorTransform.LookAt(targetPosition); // �h�A�}���̕���������
        StartCoroutine(MoveVisitor(visitorTransform, targetPosition));
    }

    private IEnumerator MoveVisitor(Transform visitorTransform, Vector3 targetPosition)
    {
        float moveSpeed = 3f; // ���K�҂̈ړ����x
        while (Vector3.Distance(visitorTransform.position, targetPosition) > 0.1f)
        {
            // �h�A�}���̕����Ɉړ�
            visitorTransform.position = Vector3.MoveTowards(visitorTransform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
