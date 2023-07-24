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
        // N2->�����_��2�ʂ܂ł̒l�Ƀt�H�[�}�b�g
        string n2 = timer.ToString("N2");
        txtStopWatch.GetComponent<TMP_Text>().text = n2;

        // �R�b��������b���\�����_��
        if (3 < timer)
        {
            txtStopWatch.SetActive(!txtStopWatch.activeSelf);
        }

        // �T�b��������B��
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
