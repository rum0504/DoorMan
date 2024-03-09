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

            // ���K�҂��h�A�}���̈ʒu�ɒ�������Q�[���I�[�o�[�ɂ���
            if (visitor.transform.position == doorManPosition)
            {
                Destroy(visitor);
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
            Destroy(oldVisitor);
        }

        // �V�������K�҂̐���
        int randomIndex = Random.Range(0, visitorPrefabs.Length);
        GameObject newVisitor = Instantiate(visitorPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        // ���K�҂�Rotation��y=180�ɌŒ肷��
        newVisitor.transform.rotation = Quaternion.Euler(0, 180, 0);
    }


}