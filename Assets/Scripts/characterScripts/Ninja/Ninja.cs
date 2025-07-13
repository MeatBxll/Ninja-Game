using System;
using System.Collections;
using UnityEngine;

public class Ninja : MonoBehaviour
{
    [SerializeField] private float abilityDurration;
    private bool abilityOffCD = true;
    [NonSerialized] public KeyCode AbilityButton = KeyCode.Q;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(AbilityButton) || abilityOffCD)
            StartCoroutine(HandleAbility());

    }

    private IEnumerator HandleAbility()
    {
        abilityOffCD = false;
        int i = 0;
        while (i < 2)
        {
            gameObject.GetComponent<playerRanged>().isSpreadShot = true;
            yield return new WaitForSeconds(abilityDurration);
        }
        abilityOffCD = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.GetComponent<playerMovement>().isDashing)
        {
            //kill enemies if is dashing into them
        }
    }
}
