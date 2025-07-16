using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizThrust : MonoBehaviour
{
    private Rigidbody2D rb;

    bool isGrounded;                    //checks if grounded variables
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float thrustCooldownTime;           //thrust cooldown variables
    public float startThrustCooldownTime;

    private bool leftThrust;                //thrust direction variables
    private bool rightThrust;
    private bool thrustAvailable;



    public float thrustSpeed;               //thrust timing variables
    private float thrustTime;
    public float startThrustTime;


    public Transform attackPos;
    public float attackRange;
    public LayerMask WhatIsEnemies;

    public int damage;
    private int baseEnemyUltCharge;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);   //checks if player is grounded

        if (thrustCooldownTime <= 0)
        {
            if (Input.GetMouseButtonDown(1) && isGrounded == true) //checks the inputs
            {
                if (Input.GetKey(KeyCode.D))
                {
                    rightThrust = true;

                }

                if (Input.GetKey(KeyCode.A) && rightThrust == false)
                {

                    leftThrust = true;

                }

                thrustAvailable = false;
            }

        }
        else
        {
            thrustCooldownTime -= Time.deltaTime;  //Timer
        }


        if (thrustTime <= 0 && thrustAvailable == false)     //executes after thrust is complete (Timer) resets everything and starts thrust timer
        {
            thrustTime = startThrustTime;
            rb.linearVelocity = Vector2.zero;
            thrustAvailable = true;
            rightThrust = false;
            leftThrust = false;

            thrustAvailable = true;

            thrustCooldownTime = startThrustCooldownTime;
        }
        else if (thrustTime > 0 && thrustAvailable == false) //executes while thrust is going (Timer)
        {
            thrustTime -= Time.deltaTime;

            if (rightThrust == true)
            {
                rb.linearVelocity = Vector2.right * thrustSpeed;
            }

            if (leftThrust == true)
            {
                rb.linearVelocity = Vector2.left * thrustSpeed;
            }



            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, WhatIsEnemies);  //hitbox for the thrust

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {

                if (enemiesToDamage[i].gameObject.layer == 9)
                {
                    enemiesToDamage[i].GetComponent<Health>().TakeDamage(damage);
                    GetComponent<SamuraiUlt>().UltimateCharge(damage);
                    thrustTime = 0;

                }

                if (enemiesToDamage[i].gameObject.layer == 6)
                {
                    GetComponent<SamuraiUlt>().UltimateCharge(baseEnemyUltCharge);
                    thrustTime = 0;
                    //enemiesToDamage[i].GetComponent<Rigidbody2D>().AddForce 
                }

            }



        }


    }


    private void OnDrawGizmosSelected()   //shows thrust hitbox
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
