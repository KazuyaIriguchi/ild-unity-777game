using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameDirector : MonoBehaviour
{
    public static int Score;
    public GameObject BallRed;
    public GameObject BallBlack;

    // �Q�[���S�̂̃^�C�}�[
    float gameTimer = 60;

    // �n�Y���̊m��
    int blackRatio = 3;  // 30%

    // ��������
    float generateWaitTime = 3;
    float generateTimer;

    GameObject txtTimer, txtScore;

    // Start is called before the first frame update
    void Start()
    {
        generateTimer = generateWaitTime;

        txtTimer = GameObject.Find("TxtTimer");
        txtScore = GameObject.Find("TxtScore");
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[���^�C�}�[
        gameTimer -= Time.deltaTime;
        txtTimer.GetComponent<TMP_Text>().text = gameTimer.ToString("F1");

        // �{�[�������^�C�}�[
        generateTimer -= Time.deltaTime;
        if (0 > generateTimer)
        {
            // �{�[������
            int rnd = Random.Range(0, 10);
            GameObject ins;
            if (rnd < blackRatio)
                {
                ins = Instantiate(BallBlack);
            }
            else
            {
                ins = Instantiate(BallRed);
            }

            // �����_���Ȉʒu�ɔz�u
            float x = Random.Range(-1, 2);
            float y = (0 == Random.Range(0, 2)) ? 0.25f : 1.5f;
            ins.transform.position = new Vector3(x, y, 10);

            float next = generateWaitTime;

            generateTimer = next;
        }
    }
}
