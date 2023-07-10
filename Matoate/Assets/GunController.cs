using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject prefabBullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // マウスが離されたとき
        if (Input.GetMouseButtonUp(0))
        {
            // 離されたマウスの場所へレイ（光線）を飛ばす
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // ベクトルを取得（ワールド座標）
            Vector3 dir = ray.direction;

            // ゲームオブジェクトを生成する
            GameObject bullet = Instantiate(prefabBullet);

            // 位置を指定
            bullet.transform.position = transform.position;

            // 発射 (3000は力）
            bullet.GetComponent<BulletController>().Shoot(dir.normalized * 3000);
        }
    }
}
