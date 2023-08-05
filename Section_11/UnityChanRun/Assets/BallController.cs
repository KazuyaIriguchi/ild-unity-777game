using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speedZ = -0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 現在のオブジェクトの位置を基準にして移動
        transform.Translate(0, 0, speedZ);

        // 画面外に出たら消す
        if (transform.position.z < -5.0f)
        {
            Destroy(gameObject);
        }
    }
}
