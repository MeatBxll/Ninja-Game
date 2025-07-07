using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlWind : MonoBehaviour
{


    public int completeUltCharge;       //ult charge Total in damage
    private int currentUltCharge;

    private bool ultAvailable;


    private float whirlwindTime;        //How long whirlwind in running (Timer)
    public float startWhirlwindTime;

    public Transform spinPos;           //Spin hitbox
    public float spinRange;
    public LayerMask WhatIsEnemies;

    public int damage;

    private bool stunned;
    private float currentStunTime;
    public float completeStunTime;

    private bool bossInvinsible;
    private float currentBossInvinsibleTime;
    public float completeBossInvinsibleTime;


    void Start()
    {
        
        ultAvailable = false;

    }

    void Update()
    {                              
           
        
       if(whirlwindTime <= 0 && ultAvailable == true)       //Timer for ult
       {
            ultAvailable = false;

            GetComponent<StaffAttack>().enabled = true;
            GetComponent<WhirlWind>().enabled = true;
        }
       else if(ultAvailable == true)
       {

            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(spinPos.position, spinRange, WhatIsEnemies);

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {

                if (enemiesToDamage[i].gameObject.layer == 9 && bossInvinsible == false)
                {
                    enemiesToDamage[i].GetComponent<Health>().TakeDamage(damage);
                    bossInvinsible = true;
                }

                if (enemiesToDamage[i].gameObject.layer == 6)
                {
                    enemiesToDamage[i].GetComponent<BaseEnemyHealth>().DestroyEnemy();
                }

            }

            whirlwindTime -= Time.deltaTime;

        }

        if (currentUltCharge >= completeUltCharge)
        {
            if (Input.GetKey(KeyCode.Q))                //checks if the requirements are met for ult
            {
                ultAvailable = true;
                whirlwindTime = startWhirlwindTime;
                currentUltCharge = 0;

                GetComponent<StaffAttack>().enabled = false;
                GetComponent<SpinAttack>().enabled = false;
            }

        }

        if (currentBossInvinsibleTime <= 0 && bossInvinsible == true)
        {
            bossInvinsible = false;
        } 
        else if (bossInvinsible == true)
        {
            currentBossInvinsibleTime -= Time.deltaTime;
        }
        



        if (currentStunTime <= 0 && stunned == true)        //delay after getting hit during ult before getting abilities back
        {
            GetComponent<StaffAttack>().enabled =true;
            GetComponent<SpinAttack>().enabled = true;
            stunned = false;
        }
        else if (stunned == true)
        {
            currentStunTime -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 6 || collision.gameObject.layer == 9 && ultAvailable == true)
        {
            ultAvailable = false;
            stunned = true;
            currentStunTime = completeStunTime;
        }
    }


    public void UltimateCharge(int damage)                                      //keeps track of ult charge
    {      
        if(currentUltCharge < completeUltCharge && ultAvailable == false)
        {
            currentUltCharge += damage;
            
        }
                                   
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(spinPos.position, spinRange);
    }

}
