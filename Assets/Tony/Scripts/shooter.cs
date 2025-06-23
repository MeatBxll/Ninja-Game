using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
    public GameObject bullet;
    public float fireRate;
    public float bulletSpeed;
    public float roamSpeed;

    private GameObject currentbullet;
    private float shootDistance = 3;
    private float groundDetectDistance = 4;
    private float nextFire;
    private Rigidbody2D rb;

    private float currentAngle = 180;
    private float holder = -180;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextFire = Time.time;
    }

    void Update()
    {
        if (Time.time > nextFire)
        {
            currentbullet = Instantiate(bullet, new Vector2(transform.position.x - shootDistance, transform.position.y), Quaternion.Euler(0, currentAngle, 0));
            currentbullet.GetComponent<enemyBullet>().speed = bulletSpeed;
            nextFire = Time.time + fireRate;
        }

        rb.velocity = new Vector2(-roamSpeed, rb.velocity.y);
        RaycastHit2D groundInfo = Physics2D.Raycast(new Vector2(transform.position.x - groundDetectDistance, transform.position.y - transform.localScale.y / 2 + .1f), Vector2.down, 10);
        if (groundInfo.collider == false)
        {
            roamSpeed = roamSpeed * -1;
            groundDetectDistance = groundDetectDistance * -1;
            shootDistance = shootDistance * -1;
            holder = holder * -1;
            currentAngle = currentAngle + holder;
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
                    shootDistance = shootDistance * -1;
                    holder = holder * -1;
                    currentAngle = currentAngle + holder;
                }
            }
        }
    }

}
