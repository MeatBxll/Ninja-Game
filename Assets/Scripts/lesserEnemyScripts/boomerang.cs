using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomerang : MonoBehaviour
{
    public float speed;
    public Transform ranger;
    public float maxDis;
    
    private float startX;

    private void Start()
    {
        startX = transform.position.x;
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if(ranger == null)
        {
            Destroy(gameObject);
        }
        else
        {
            if (Mathf.Abs(transform.position.x - startX) > maxDis)
            {
                Vector2 moveDirection = (ranger.position - transform.position).normalized * speed;
                GetComponent<Rigidbody2D>().linearVelocity = new Vector2(moveDirection.x, moveDirection.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
