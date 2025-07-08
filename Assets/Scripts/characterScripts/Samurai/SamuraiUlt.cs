using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiUlt : MonoBehaviour
{
    private Rigidbody2D rb;

    public Transform ultPos;      //used for Ulthitbox function (Hitbox)
    public float ultRange;
    public LayerMask WhatIsEnemies;

    private float samuraiUltTimeUp;             //Timer going upward and the speed multiplier
    public float startSamuraiUltTimeUp;
    public float upSpeed;

    bool isGrounded;                            //checks if the player touches the ground
    public Transform groundCheck;
    public LayerMask groundLayer;
    
    public int completeUltCharge;       //ult charge Total in damage
    private int currentUltCharge;

    private bool ultAvailable;  

    private bool downSlam;              //used for on the way down and speed multiplier
    public float downSpeed;

    public GameObject projectile;       //Used for Shockwaves
    public Transform ultShotPoint1;
    public Transform ultShotPoint2;

    public int damage;


    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        ultAvailable = false;       

    }

    // Update is called once per frame
    void Update()
    {


        if(samuraiUltTimeUp <= 0 && ultAvailable == true && downSlam == false)         //Timer
        {
            rb.gravityScale = 0.0f;                                                    //Executes after the upward timer is over and start downslam timer
            rb.velocity = Vector2.zero;

            downSlam = true;
        }
        else if(ultAvailable == true && downSlam == false)
        {
            UltHitbox();
            rb.velocity = Vector2.up * upSpeed;                                        //executes while going upward (Timer)
            
            GetComponent<playerMovement>().enabled = false;
            GetComponent<Dash>().enabled = false;
            GetComponent<SwordSwing>().enabled = false;
            GetComponent<HorizThrust>().enabled = false;

            samuraiUltTimeUp -= Time.deltaTime;

        }

        if (downSlam == true)
        {

            rb.velocity = Vector2.down * downSpeed;                                    //Executes while going down. Checks if grounded
            UltHitbox();
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

            if(isGrounded)
            {
                rb.gravityScale = 1.0f;                                                //resets everything once grounded
                downSlam = false;
                ultAvailable = false;

                GetComponent<playerMovement>().enabled = true;
                GetComponent<Dash>().enabled = true;
                GetComponent<SwordSwing>().enabled = true;
                GetComponent<HorizThrust>().enabled = true;             

                Instantiate(projectile, ultShotPoint1.position, transform.rotation); //Spawns the Shockwaves
                Instantiate(projectile, ultShotPoint2.position, transform.rotation);
            }
        }
              

        if (currentUltCharge >= completeUltCharge)
        {
            if (Input.GetKey(KeyCode.Q))                //checks if the requirements are met for ult
            {
                ultAvailable = true;
                samuraiUltTimeUp = startSamuraiUltTimeUp;
                currentUltCharge = 0;
            }

        }

    }

    public void UltimateCharge(int damage)                                      //keeps track of ult charge
    {
        if (currentUltCharge < completeUltCharge && ultAvailable == false)
        {
            currentUltCharge += damage;           
        }

    }

    private void UltHitbox()            //hitbox
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(ultPos.position, ultRange, WhatIsEnemies);

        for (int i = 0; i < enemiesToDamage.Length; i++)
        {

            if (enemiesToDamage[i].gameObject.layer == 9)
            {
                enemiesToDamage[i].GetComponent<Health>().TakeDamage(damage);
            }

            if (enemiesToDamage[i].gameObject.layer == 6)
            {
                enemiesToDamage[i].GetComponent<BaseEnemyHealth>().DestroyEnemy();
            }

        }
    }

    private void OnDrawGizmosSelected()             
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(ultPos.position, ultRange);
    }



}
