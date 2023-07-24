using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject objGround;
    public GameObject txtInfo;

    Vector2 startPos;
    Rigidbody2D rb;

    // 摩擦の値
    float frictionForce = 0.98f;
    bool gameEnd;

    int touchCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnd) return;

        if (Input.GetMouseButtonDown(0))
        {
            // 開始地点を取得
            startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (0 < touchCount)
            {
                // 終了地点を取得
                Vector2 endPos = Input.mousePosition;
                // 加算する値
                Vector2 addForce = endPos - startPos;

                rb.AddForce(addForce);

                touchCount--;
            }
        }

        // 摩擦をつけて少しづつ減速させる
        rb.velocity *= frictionForce;

        // テキスト更新
        float sx = transform.position.x;
        float sy = transform.position.y;
        float ex = objGround.transform.localScale.x / 2;
        float dx = ex - sx;

        txtInfo.GetComponent<TMP_Text>().text = dx.ToString("N1") + " m";

        // ゴール判定
        if (0 < dx && dx < 1)
        {
            // 停止判定
            if (0.01f > rb.velocity.x)
            {
                txtInfo.GetComponent<TMP_Text>().text = "Success";

                // ゴール演出
                txtInfo.GetComponent<TMP_Text>().color = Color.black;
                GetComponent<SpriteRenderer>().color = Color.black;
                objGround.GetComponent<SpriteRenderer>().color = Color.black;
                Camera.main.GetComponent<Camera>().backgroundColor = Color.white;

                gameEnd = true;
            }
        }
        else if (-10> sy)
        {
            // 下に落ちたら失敗
            txtInfo.GetComponent<TMP_Text>().text = "Failure...";
            gameEnd = true;
        }
    }

    // RETRYボタンの処理
    public void OnRetryButtonPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
