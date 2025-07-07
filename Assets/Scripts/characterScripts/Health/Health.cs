using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 1000;
    public int currentHealth;
    public float halfHealth;

    private healthBar healthBar;
    void Start()
    {
        healthBar = GameObject.FindWithTag("healthBar").GetComponent<healthBar>();

        halfHealth = maxHealth / 2;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void GainHealth(int healthGained)
    {
        if (healthGained == -1)
        {
            currentHealth = maxHealth;
        }

        if (healthGained != -1)
        {
            currentHealth += healthGained;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
