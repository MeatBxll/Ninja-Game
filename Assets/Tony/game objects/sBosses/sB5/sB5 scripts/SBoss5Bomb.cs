using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBoss5Bomb : MonoBehaviour
{
    public GameObject SBoss5BombExplosion;
    public float bombDropSpeed;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.down * bombDropSpeed, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 4);
        Invoke("MakeBoom", 3);



    }

    void MakeBoom()
    {
        Instantiate(SBoss5BombExplosion, transform.position, transform.rotation);
    }
}
