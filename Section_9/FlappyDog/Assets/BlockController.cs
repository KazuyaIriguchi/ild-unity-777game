using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public float moveSpeed = -100;

    Vector2 screenMin;

    // Start is called before the first frame update
    void Start()
    {
        screenMin = Camera.main.ViewportToWorldPoint(Vector2.zero);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(moveSpeed, 0));
    }

    // Update is called once per frame
    void Update()
    {
        // ‰æ–ÊŠO‚Ì”»’è
        if (transform.position.x * transform.localScale.x < screenMin.x)
        {
            Destroy(gameObject);
        }
    }
}
