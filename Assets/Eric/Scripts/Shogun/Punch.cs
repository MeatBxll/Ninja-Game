using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{

    public Transform attackPos;
    public float attackRange;
    public LayerMask WhatIsEnemies;

    public int damage;

    public bool ultInUse;

    private bool usingPunch;

    private bool inBetweenPunches;

    public int numberOfPunches;
    private int currentPunch;

    private float punchCooldown;            // First Timer Iverall Punch
    public float startPunchCooldown;

    private float punchOutTime;             //Second Timer How long punch is out
    public float startPunchOutTime;

    private float timeBtwAttack;            //Third Timr how long between each punch
    public float startTimeBtwAttack;

    void Start()
    {
        punchCooldown = 0;
        currentPunch = numberOfPunches;
    }


    void Update()
    {

        if (ultInUse == false)   //Checks if using ult
        {

            if (punchCooldown <= 0)         //Timer for punch cooldown
            {

                if (Input.GetMouseButtonDown(0) && usingPunch == false && currentPunch > 0)     //if punch button pressed set timers, trigger the shot, and set appropriate variables
                {
                    currentPunch--;
                    punchOutTime = startPunchOutTime;
                    GameObject.Find("Hand").GetComponent<ShogunHand>().windPunch = true;
                    usingPunch = true;
                    inBetweenPunches = false;
                }
           
                if (punchOutTime <= 0 && usingPunch == true)        // Timer for how long the punch is out
                {
                                       
                    if (currentPunch <= 0)                          // If their are no more punches start punch cooldown and reset some variables
                    {
                        currentPunch = numberOfPunches;
                        punchCooldown = startPunchCooldown;
                        currentPunch = numberOfPunches;
                    }

                    inBetweenPunches = true;
                    usingPunch = false;
                    timeBtwAttack = startTimeBtwAttack;

                }
                else if (usingPunch == true)
                {
                    UltHitbox();                   
                    punchOutTime -= Time.deltaTime;
                }

                if (timeBtwAttack <= 0 && inBetweenPunches == true)         //Timer for how long you have between each punch
                {
                    currentPunch = numberOfPunches;
                    punchCooldown = startPunchCooldown;
                    currentPunch = numberOfPunches;                 // This Timer keeps repeating needs fix
                    Debug.Log("Out of Time");     
                }
                else if (inBetweenPunches == true)
                {
                    timeBtwAttack -= Time.deltaTime;
                }

            }
            else if (usingPunch == false)
            {
                punchCooldown -= Time.deltaTime;        
            }

        }

    }

    private void UltHitbox()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, WhatIsEnemies);  //Checks if the enemy has collided with hitbox

        for (int i = 0; i < enemiesToDamage.Length; i++)        //loop for all enemeies in hitbox
        {
            enemiesToDamage[i].GetComponent<Health>().TakeDamage(damage);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }


}










