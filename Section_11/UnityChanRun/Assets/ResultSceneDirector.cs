using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultSceneDirector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("ResultScore").GetComponent<TMP_Text>().text = "SCORE: " + GameDirector.Score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRetryButtonPressed()
    {
        // ç≈èâÇÃÉVÅ[ÉìÇ…ñﬂÇÈ
        SceneManager.LoadScene("SampleScene");

    }
}
