using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject prefabBlock;

    // timer of generate block
    float waitTimer;

    // elapsed time
    public float totalTimer;

    // stop state
    public bool isStop;

    // Start is called before the first frame update
    void Start()
    {
        isStop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop) return;

        waitTimer -= Time.deltaTime;
        totalTimer += Time.deltaTime;

        if (0 > waitTimer)
        {
            // generate block
            Vector3 pos = transform.position;
            pos.y = Random.Range(-5, 5);

            // generate prefab
            GameObject obj = Instantiate(prefabBlock, pos, Quaternion.identity);
            BlockController ctrl = obj.GetComponent<BlockController>();
            ctrl.moveSpeed = -(100 + (totalTimer * 1.5f));

            // next to generate time
            float min = 2;
            if (0.01f > min) min = 0.01f;
            float max = min * 2;
            waitTimer = Random.Range(min, max);
        }
    }
}
