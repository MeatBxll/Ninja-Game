using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ntParabolaBullet : MonoBehaviour
{
    public float speed;

    private float holder;

    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
        Destroy(gameObject, 4);
        holder = Time.time + .2f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(holder < Time.time)
        {
            Destroy(gameObject);
        }
    }
}
