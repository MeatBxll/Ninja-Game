using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombTosser : MonoBehaviour
{
    public float roamSpeed;
    public float fireRate;
    public float bombSpeed;
    public float tossAngle;
    public float bombFallSpeed;

    public GameObject bomb;

    private GameObject currentBomb;
    private float shootDistance = 2;
    
    
    private Rigidbody2D rb;
    private float nextFire;
    private float groundDetectDistance = 4;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextFire = Time.time;
    }

    void Update()
    {
        if (Time.time > nextFire)
        {
            currentBomb = Instantiate(bomb, new Vector2(transform.position.x - shootDistance , transform.position.y), Quaternion.Euler(transform.rotation.x , transform.rotation.y, tossAngle));
            currentBomb.GetComponent<bomb>().speed = bombSpeed;
            currentBomb.GetComponent<Rigidbody2D>().gravityScale = bombFallSpeed;
            nextFire = Time.time + fireRate;
        }

        rb.linearVelocity = new Vector2(-roamSpeed, rb.linearVelocity.y);
        RaycastHit2D groundInfo = Physics2D.Raycast(new Vector2(transform.position.x - groundDetectDistance, transform.position.y - transform.localScale.y / 2 + .1f), Vector2.down, 10);
        if (groundInfo.collider == false)
        {
            roamSpeed = roamSpeed * -1;
            groundDetectDistance = groundDetectDistance * -1;
            tossAngle = tossAngle * -1;
            shootDistance = shootDistance * -1;
        }
        RaycastHit2D sideInfo = Physics2D.Raycast(new Vector2(transform.position.x - groundDetectDistance, transform.position.y + transform.localScale.y / 2), Vector2.down, transform.localScale.y - .4f);
        if (sideInfo.collider != false)
        {
            if (sideInfo.collider.gameObject.tag != "Player")
            {
                if (sideInfo.collider.gameObject.layer != 8)
                {
                    roamSpeed = roamSpeed * -1;
                    groundDetectDistance = groundDetectDistance * -1;
                    tossAngle = tossAngle * -1;
                    shootDistance = shootDistance * -1;
                }
            }
        }

    }
}
