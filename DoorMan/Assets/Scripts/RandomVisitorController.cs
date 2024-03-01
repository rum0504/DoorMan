using UnityEngine;
using System.Collections;

public class RandomVisitorController : MonoBehaviour
{
    public GameObject[] visitorPrefabs; // �C���X�y�N�^�[����A�^�b�`���ꂽ���K�҂̃v���n�u
    public float spawnInterval = 3f; // ���K�҂̐����Ԋu
    public Vector3 doorManPosition; // �h�A�}���̍��W

    private Coroutine spawnCoroutine; // ���K�҂̐����R���[�`���̎Q��

    private void Start()
    {
        // �C���^�[�o�����Ƃɗ��K�҂𐶐�����R���[�`�����J�n
        spawnCoroutine = StartCoroutine(SpawnVisitors());
    }

    private void OnDestroy()
    {
        // �I�u�W�F�N�g���j�������Ƃ��ɃR���[�`������~����
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    public IEnumerator SpawnVisitors()
    {
        while (true)
        {
            // �v���n�u���烉���_���ɗ��K�҂�I��
            GameObject randomVisitorPrefab = visitorPrefabs[Random.Range(0, visitorPrefabs.Length)];

            // �v���n�u���痈�K�҂𐶐�
            GameObject newVisitor = Instantiate(randomVisitorPrefab, transform.position, Quaternion.identity);

            // �h�A�}���̕����Ɍ������ĕ�������
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
        float moveSpeed = 3f; // ���K�҂̈ړ����x
        while (Vector3.Distance(visitorTransform.position, targetPosition) > 0.1f)
        {
            // �h�A�}���̕����Ɉړ�
            visitorTransform.position = Vector3.MoveTowards(visitorTransform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // �ړI�n�ɓ��B������I�u�W�F�N�g��j������
        Destroy(visitorTransform.gameObject);
    }

    // �V�������K�҂𐶐����郁�\�b�h
    public void SpawnNewVisitor()
    {
        GameObject randomVisitorPrefab = visitorPrefabs[Random.Range(0, visitorPrefabs.Length)];
        GameObject newVisitor = Instantiate(randomVisitorPrefab, transform.position, Quaternion.identity);
        MoveTowardsDoorMan(newVisitor.transform);
    }
}
