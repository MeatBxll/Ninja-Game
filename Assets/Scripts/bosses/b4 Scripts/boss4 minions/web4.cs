using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class web4 : MonoBehaviour
{
    private GameObject boss4;
    private Rigidbody2D rb;
    public bool middle;
    public float speed;
    public float height;
    public float destryTime;
    public float freezeTime;
    void Start()
    {
        boss4 = GameObject.FindWithTag("boss4");
        rb = GetComponent<Rigidbody2D>();
        if (middle == false)
        {
            if (transform.position.x < boss4.transform.position.x)
            {
                rb.velocity = new Vector2(-speed, height);

            }
            else
            {
                rb.velocity = new Vector2(speed, height);
            }
        }
        Destroy(gameObject, destryTime);
        Invoke("freeze", freezeTime);
    }
    void freeze()
    {
        rb.velocity = new Vector2(0, 0);
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
    }
}
