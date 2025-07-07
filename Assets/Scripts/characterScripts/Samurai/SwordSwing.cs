using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask WhatIsEnemies;

    public int damage;
    private int baseEnemyUltCharge;



    void Start()
    {

    }


    void Update()
    {



        if (timeBtwAttack <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, WhatIsEnemies);

                for (int i = 0; i < enemiesToDamage.Length; i++)
                {

                    if (enemiesToDamage[i].gameObject.layer == 9)
                    {
                        enemiesToDamage[i].GetComponent<Health>().TakeDamage(damage);
                        GetComponent<SamuraiUlt>().UltimateCharge(damage);
                    }

                    if (enemiesToDamage[i].gameObject.layer == 6)
                    {
                        enemiesToDamage[i].GetComponent<BaseEnemyHealth>().DestroyEnemy();
                        baseEnemyUltCharge = enemiesToDamage[i].GetComponent<BaseEnemyHealth>().ultCharge;
                        GetComponent<SamuraiUlt>().UltimateCharge(baseEnemyUltCharge);
                    }

                }

                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }


    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
