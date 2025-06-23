using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public GameObject boom;
    public float speed;

    private float timeHolder;

    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
        timeHolder = Time.time + 3;
    }

    void Update()
    {
        if(Time.time > timeHolder)
        {
            Instantiate(boom , transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }
}
