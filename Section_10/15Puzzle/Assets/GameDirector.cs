using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    // タイル関連
    List<GameObject> tiles;
    List<Vector2> startPositions;
    public int shuffleCount = 2;

    // clear flag
    bool isClear;
    GameObject txtInfo;

    // Start is called before the first frame update
    void Start()
    {
        txtInfo = GameObject.Find("Text (TMP)");
        txtInfo.SetActive(false);

        tiles = new List<GameObject>();
        startPositions = new List<Vector2>();

        // タイル関連
        for (int i = 0; i < 16; i++)
        {
            GameObject obj = GameObject.Find("" + i);
            tiles.Add(obj);

            // 正解のポジション（初期状態）
            startPositions.Add(obj.transform.position);
        }

        // シャッフル処理
        for (int i = 0; i < shuffleCount; i++)
        {
            List<GameObject> moves = new List<GameObject>();

            // すべてのオブジェクト
            foreach (GameObject obj in tiles)
            {
                // 0に隣接しているオブジェクトを取得
                if (null != GetExTile(obj))
                {
                    moves.Add(obj);
                }
            }

            if ( 0 < moves.Count)
            {
                // ランダムで1つ動かす
                int rnd = Random.Range(0, moves.Count);
                GameObject tile0 = GetExTile(moves[rnd]);

                ChangeTile(moves[rnd], tile0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isClear) return;

        // クリア判定
        isClear = true;
        for (int i = 0; i < tiles.Count; i++)
        {
            Vector2 pos = tiles[i].transform.position;
            // 最初に保存したタイルの位置と同じかを判定
            if (startPositions[i] != pos)
            {
                isClear = false;
            }
        }

        if (isClear)
        {
            txtInfo.SetActive(true);

            // 弾ける処理
            for (int i = 0; i < tiles.Count; i++)
            {
                float x = Random.Range(-30, 30);
                float y = Random.Range(-30, 30);
                tiles[i].AddComponent<Rigidbody2D>().velocity = new Vector2(x, y);
            }
        }

        // タッチ処理
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10);

            if (hit.collider)
            {
                // ヒットしたオブジェクトを取得
                GameObject hitObj = hit.collider.gameObject;
                // 入れ替え可能なオブジェクトを取得
                GameObject target = GetExTile(hitObj);

                // 入れ替え
                ChangeTile(hitObj, target);
            }
        }
    }

    // 入れ替え可能なタイルを見つけた場合、それを返す
    GameObject GetExTile(GameObject tile)
    {
        GameObject ret = null;

        Vector2 posa = tile.transform.position;

        foreach (var obj in tiles)
        {
            Vector2 posb = obj.transform.position;
            float dist = Vector2.Distance(posa, posb);

            // 隣接するタイルとの距離が1で、かつ"0"のタイルのとき
            if ( 1 == dist && obj.name.Equals("0"))
            {
                ret = obj;
            }
        }

        return ret;
    }

    // 場所を入れ替える
    void ChangeTile(GameObject tileA, GameObject tileB)
    {
        if (null == tileA || null == tileB) return;

        // ポジション更新 位置を入れ替える
        Vector2 tmp = tileA.transform.position;
        tileA.transform.position = tileB.transform.position;
        tileB.transform.position = tmp;
    }

    // リトライ（シーン再読み込み）
    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
