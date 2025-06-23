using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomerRanger : MonoBehaviour
{
    public float roamSpeed;
    public float fireRate;
    public float boomerSpeed;
    public float tossAngle;
    public float returnDistance;
    public float fallSpeed;

    public GameObject boomerang;

    private GameObject currentBullet;
    private float shootDistance = 4;


    private float nextFire;
    private float groundDetectDistance = 4;


    void Update()
    {
        if (Time.time > nextFire)
        {
            currentBullet = Instantiate(boomerang, new Vector2(transform.position.x - shootDistance, transform.position.y), Quaternion.Euler(transform.rotation.x, transform.rotation.y, tossAngle));
            currentBullet.GetComponent<boomerang>().speed = boomerSpeed;
            currentBullet.GetComponent<boomerang>().ranger = transform;
            currentBullet.GetComponent<boomerang>().maxDis = returnDistance;

            currentBullet.GetComponent<Rigidbody2D>().gravityScale = fallSpeed;
            nextFire = Time.time + fireRate;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(-roamSpeed, GetComponent<Rigidbody2D>().velocity.y);
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
