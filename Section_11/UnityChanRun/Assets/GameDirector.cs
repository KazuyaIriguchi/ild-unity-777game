using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public static int Score;
    public GameObject BallRed;
    public GameObject BallBlack;

    // ゲーム全体のタイマー
    float gameTimer = 60;

    // ハズレの確率
    int blackRatio = 3;  // 30%

    // 生成時間
    float generateWaitTime = 3;
    float generateTimer;

    GameObject txtTimer, txtScore;

    // Start is called before the first frame update
    void Start()
    {
        generateTimer = generateWaitTime;

        txtTimer = GameObject.Find("TxtTimer");
        txtScore = GameObject.Find("TxtScore");

        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームタイマー
        gameTimer -= Time.deltaTime;
        txtTimer.GetComponent<TMP_Text>().text = gameTimer.ToString("F1");

        // ボール生成タイマー
        generateTimer -= Time.deltaTime;
        if (0 > generateTimer)
        {
            // ボール生成
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

            // ランダムな位置に配置
            float x = Random.Range(-1, 2);
            float y = (0 == Random.Range(0, 2)) ? 0.25f : 1.5f;
            ins.transform.position = new Vector3(x, y, 10);

            // 次の生成時間
            float next = generateWaitTime;

            // 時間が経過するとだんだん生成速度が縮まる
            if (10 > gameTimer)
            {
                next = generateWaitTime * 0.1f;
            }
            else if (20 > gameTimer)
            {
                next = generateWaitTime * 0.2f;
            }
            else if (30 > gameTimer)
            {
                next = generateWaitTime * 0.3f;
            }
            else if (40 > gameTimer)
            {
                next = generateWaitTime * 0.4f;
            }
            else if (50 > gameTimer)
            {
                next = generateWaitTime * 0.5f;
            }

            generateTimer = next;
        }

        // ゲーム終了判定
        if (0 > gameTimer)
        {
            // リザルトシーンに繊維
            SceneManager.LoadScene("ResultScene");
        }
    }

    // スコア加算
    public void AddScore(int v = 100)
    {
        Score += v;
        txtScore.GetComponent<TMP_Text>().text = "SCORE: " + Score;
    }
}
