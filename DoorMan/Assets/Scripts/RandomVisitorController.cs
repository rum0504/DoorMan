using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomVisitorController : MonoBehaviour
{
    public GameObject[] visitorPrefabs; // ���K�҂̃v���n�u�i�����j
    public Vector3 spawnPosition = new Vector3(0, -1.5f, 5); // ���K�҂̐����ʒu
    public Vector3 doorManPosition = new Vector3(0, -1.5f, -7); // �h�A�}���̈ʒu
    public Vector3 damagePosition = new Vector3(0, -1.5f, -5); //�_���[�W������ʒu
    public float speed = 1.0f; // ���K�҂̈ړ����x

    public GameManager gameManager; // GameManager�ւ̎Q�Ƃ�ێ�����ϐ�
    GameObject currentVisitor; // ���݂̗��K�҂�ێ�����ϐ�

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
            // ���K�҂��h�A�}���̈ʒu�Ɍ������Ĉړ�
            float step = speed * Time.deltaTime;
            visitor.transform.position = Vector3.MoveTowards(visitor.transform.position, doorManPosition, step);

            if(visitor.transform.position == damagePosition)
            {
                StartCoroutine(ColorCoroutine()); // �Ԃ��_�ł�����Coroutine���J�n
            }

            // ���K�҂��h�A�}���̈ʒu�ɒ�������Q�[���I�[�o�[�ɂ���
            if (visitor.transform.position == doorManPosition)
            {
                StopCoroutine(ColorCoroutine());
                gameManager.GameOver(); // GameManager��GameOver�֐����Ăяo��
            }
        }
    }

    public void SpawnVisitor()
    {
        // ���K�҂����݂���ꍇ�͍폜
        GameObject oldVisitor = GameObject.FindGameObjectWithTag("Visitor");
        if (oldVisitor != null)
        {
            StopCoroutine(ColorCoroutine());
            Destroy(oldVisitor);
        }

        // �V�������K�҂̐���
        int randomIndex = Random.Range(0, visitorPrefabs.Length);
        GameObject newVisitor = Instantiate(visitorPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        // ���K�҂�Rotation��y=180�ɌŒ肷��
        newVisitor.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    IEnumerator ColorCoroutine()
    {
        while (true)
        {
            // �Ԃ��_�ł����鏈��
            Color redColor = Color.red;
            redColor.a = 0.5f; // �����x��ݒ肷��i0.0����1.0�̊ԂŎw��j
            redImage.color = redColor;
            yield return new WaitForSeconds(0.1f);

            Color whiteColor = Color.white;
            whiteColor.a = 0f; // �����x��ݒ肷��i0.0����1.0�̊ԂŎw��j
            redImage.color = whiteColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

}
