using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class GameDirector : MonoBehaviour
{
    public GameObject player;
    public GameObject textScore;
    public GameObject textInfo;
    public GameObject btnRetry;
    public BlockManager blockManager;

    float startTimer;

    enum MODE
    {
        NONE,
        READY,
        MAIN,
        RESULT
    };
    MODE nowMode, nextMode;

    // Start is called before the first frame update
    void Start()
    {
        startTimer = 3.9f;
        btnRetry.SetActive(false);
        // 最初は重力をオフにし、ゲームが始まったらONにする
        player.GetComponent<Rigidbody2D>().simulated = false;
        nowMode = MODE.READY;
        nextMode = MODE.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        if (MODE.READY == nowMode)
        {
            // countdown before start
            startTimer -= Time.deltaTime;
            textInfo.GetComponent<TMP_Text>().text = "" + Mathf.Floor(startTimer);

            if (1 > startTimer)
            {
                textInfo.GetComponent<TMP_Text>().text = "START!";
            }

            if (0 > startTimer)
            {
                // enable kinematic
                player.GetComponent<Rigidbody2D>().simulated = true;
                // start to generate block
                blockManager.isStop = false;
                textInfo.SetActive(false);
                nextMode = MODE.MAIN;
            }
        }
        else if (MODE.MAIN == nowMode)
        {
            // game over
            if (null == player)
            {
                textInfo.GetComponent<TMP_Text>().text = "GAME OVER";
                textInfo.SetActive(true);
                btnRetry.SetActive(true);
                nextMode = MODE.RESULT;
            }

            // update score
            textScore.GetComponent<TMP_Text>().text = "" + Mathf.Floor(blockManager.totalTimer);
        }

        // switch mode
        if (MODE.NONE != nextMode)
        {
            nowMode = nextMode;
            nextMode = MODE.NONE;
        }
    }

    public void OnRetryButtonPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
