using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : MonoBehaviour
{
    
    private float spinCooldown;         //Spin cooldown (Timer)
    public float startSpinCooldown;

    private float spinTime;             //How long the spin is active (Timer)
    public float startSpinTime;

    private bool spinAvailable;        

    public Transform spinPos;           //Spin hitbox
    public float spinRange;
    public LayerMask WhatIsEnemies;

    public int damage;
    private int baseEnemyUltCharge;


    void Start()
    {

    }


    void Update()
    {



        if(spinCooldown <= 0 && spinAvailable == true)          //checks if the spin is on cooldown (Timer)
        {    
            if (Input.GetMouseButtonDown(1))
            {

                spinAvailable = false;
                spinTime = startSpinTime;

            }
        }
        else if(spinAvailable == true)
        {
            spinCooldown -= Time.deltaTime;
        }



        if (spinTime <= 0 && spinAvailable == false)          //Executes while the spin is going (Timer)
        {
            spinCooldown = startSpinCooldown;
            spinAvailable = true;
        }
        else if(spinAvailable == false)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(spinPos.position, spinRange, WhatIsEnemies);

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {

                if (enemiesToDamage[i].gameObject.layer == 9)
                {
                    enemiesToDamage[i].GetComponent<Health>().TakeDamage(damage);
                    GetComponent<WhirlWind>().UltimateCharge(damage);
                }

                if (enemiesToDamage[i].gameObject.layer == 6)
                {
                    enemiesToDamage[i].GetComponent<BaseEnemyHealth>().DestroyEnemy();
                    baseEnemyUltCharge = enemiesToDamage[i].GetComponent<BaseEnemyHealth>().ultCharge;
                    GetComponent<WhirlWind>().UltimateCharge(baseEnemyUltCharge);
                }

            }

            spinTime -= Time.deltaTime;

        }

    }



    private void OnDrawGizmosSelected()             
    {
        Gizmos.color = Color.gray;

        Gizmos.DrawWireSphere(spinPos.position, spinRange);
    }
}
