using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    private Rigidbody2D rb;

    private bool leftDash;
    private bool rightDash;
    private bool upDash;
    private bool downDash;

    private bool dashAvailable;
    
    public int numberOfDashes;
    public int currentDash;

    public int dashSpeed;
    private float dashTime;
    public float startDashTime;

    public float dashCooldown;
    private float copyDashCooldown;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        dashTime = startDashTime;

        dashAvailable = true;

        currentDash = numberOfDashes - 1;

    }

    void Update()
    {
        
        if (dashAvailable == true)
        {


            leftDash = false;
            rightDash = false;
            upDash = false;
            downDash = false;

        }


        if(Input.GetKeyDown(KeyCode.LeftShift) & dashAvailable == true & currentDash > 0)
        {

            if (Input.GetKey(KeyCode.A))
            {
                leftDash = true;               
            }

            if (Input.GetKey(KeyCode.D))
            {
                rightDash = true;               
            }

            if (Input.GetKey(KeyCode.W))
            {
                upDash = true;               
            }

            if (Input.GetKey(KeyCode.S))
            {
                downDash = true;               
            }

            dashAvailable = false;

            GetComponent<Walking>().enabled = false;

            currentDash--;
            
        }

        if (dashTime <= 0 & dashAvailable == false)
        {
            dashTime = startDashTime;
            rb.velocity = Vector2.zero;
            GetComponent<Walking>().enabled = true;
            dashAvailable = true;

        }
        else if (dashTime > 0 & dashAvailable == false)
        {

            dashTime -= Time.deltaTime;


            if (leftDash == true)
            {
                rb.velocity = Vector2.left * dashSpeed;
            }
            if (rightDash == true)
            {
                rb.velocity = Vector2.right * dashSpeed;
            }
            if (upDash == true)
            {
                rb.velocity = Vector2.up * dashSpeed;
            }
            if (downDash == true)
            {
                rb.velocity = Vector2.down * dashSpeed;
            }
        }

        
        if (currentDash < numberOfDashes)
        {
            if (copyDashCooldown > 0)
            {
                copyDashCooldown -= Time.deltaTime;
            }
            else
            {
                currentDash++;
                copyDashCooldown = dashCooldown;
                //Debug.Log("Dash Ready");
            }
        } 
    }
}
