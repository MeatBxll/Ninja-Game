using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunner : MonoBehaviour
{
    public float fireRate;
    public float bulletSpeed;
    public float roamSpeed;

    //shotgun variables
    public GameObject shell;
    public float shellSpeed = 20f;
    public GameObject[] shellAmount;
    public float spreadAngle;

    private float currentSpread;

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
            for (int i = 0; i < shellAmount.Length; i++)
            {
                shellAmount[i] = Instantiate(shell, new Vector2(transform.position.x - shootDistance, transform.position.y + 1), Quaternion.Euler(0, 0, (-currentAngle - (spreadAngle * (shellAmount.Length * 0.5f - 0.5f))) + currentSpread));
                currentSpread = spreadAngle * (i + 1);
                if (i == shellAmount.Length - 1)
                {
                    currentSpread = 0;
                }
            }

            foreach (GameObject sGAmmo in shellAmount)
            {
                sGAmmo.GetComponent<enemyBullet>().speed = shellSpeed;
            }
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

