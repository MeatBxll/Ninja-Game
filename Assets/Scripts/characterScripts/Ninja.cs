using System;
using System.Collections;
using UnityEngine;

public class Ninja : MonoBehaviour
{
    [SerializeField] private float abilityDurration;
    [SerializeField] private float abilityCooldown;
    private bool abilityOffCD = true;
    [NonSerialized] public KeyCode AbilityButton = KeyCode.Q;
    private bool isUsingAbility;

    void Update()
    {
        if (Input.GetKeyDown(AbilityButton) && abilityOffCD)
            StartCoroutine(HandleAbility());

    }

    private IEnumerator HandleAbility()
    {

        abilityOffCD = false;
        int i = 0;
        while (i < 2)
        {
            if (i == 0)
            {
                isUsingAbility = true;
                gameObject.GetComponent<playerRanged>().isSpreadShot = true;
                yield return new WaitForSeconds(abilityDurration);
            }
            else if (i == 1)
            {
                isUsingAbility = false;
                gameObject.GetComponent<playerRanged>().isSpreadShot = false;
                yield return new WaitForSeconds(abilityCooldown);

            }
            i++;

        }
        abilityOffCD = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.GetComponent<playerMovement>().isDashing && isUsingAbility)
        {
            if (col.gameObject.tag == "Enemy")
                Destroy(col.gameObject);
        }
    }
}
