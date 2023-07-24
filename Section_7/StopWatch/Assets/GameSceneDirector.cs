using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;

public class GameSceneDirector : MonoBehaviour
{
    public GameObject txtStopWatch;
    public GameObject btnStop;
    public GameObject btnReset;

    float timer;
    bool isStop;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop) return;

        timer += Time.deltaTime;
        // N2->小数点第2位までの値にフォーマット
        string n2 = timer.ToString("N2");
        txtStopWatch.GetComponent<TMP_Text>().text = n2;

        // ３秒超えたら秒数表示が点滅
        if (3 < timer)
        {
            txtStopWatch.SetActive(!txtStopWatch.activeSelf);
        }

        // ５秒超えたら隠す
        if (5 < timer)
        {
            txtStopWatch.SetActive(false);
        }
    }

    public void Stop()
    {
        isStop = true;
        txtStopWatch.SetActive(true);

        print("Score: " + timer);
    }

    public void OnResetButtonPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
