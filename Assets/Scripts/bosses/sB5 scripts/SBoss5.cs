using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBoss5 : MonoBehaviour
{

    //general stuff
    private bool leap;
    private bool thrust;
    private bool teleportBehind;
    private bool shadowClone;
    private bool bomb;
    private bool onlyOnce;
    private float holder;
    private float resetHolder;
    private Rigidbody2D rb;
    private GameObject player;

    public float resetTime;




    //leap variables
    private bool leapOnlyOnce;

    public float leapHeight;
    public float leapHorizontalForce;




    //thrust variables
    private Vector2 moveDirection;

    public float thrustSpeed;
    public float thrustDurration;




    //teleportBehind variables
    private bool behindLeft;
    private bool behindRight;

    public float behindPlayerDist;
    public float jabFromBehindSpeed;
    public float jabFromBehindDurration;




    //shadowClone variables
    private float cloneHolder;
    private float cloneResetHolder;

    public float cloneHorizontalForce;
    public float cloneDownForce;
    public GameObject clone;
    public float cloneHorizontalDist;
    public float cloneHeight;




    //bomb variables
    private float leftDashHolder;
    private float rightDashHolder;
    private bool onlyOneBomb;

    public GameObject bombGameObject;
    public float bombDownForce;
    public float bombResetTime;
    public float bombUpForce;
    public float bombDashSpeed;
    public float SecondBombDashSpeed;
    public float bombDashTime;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        resetBoss();
        //leap = true;
        //thrust = true;
        //teleportBehind = true;
        //shadowClone = true;
        //bomb = true;

    }

    void Update()
    {

        player = GameObject.FindWithTag("Player");

        if (holder != 0)
        {
            if (holder < Time.time)
            {
                resetHolder = Random.Range(1, 5);
                holder = 0;

            }


            if (resetHolder != 0)
            {
                if (resetHolder == 1)
                {
                    resetHolder = 0;
                    thrust = true;
                    teleportBehind = false;
                    shadowClone = false;
                    bomb = false;
                    onlyOnce = false;

                }
                else if (resetHolder == 2)
                {
                    resetHolder = 0;
                    thrust = false;
                    teleportBehind = true;
                    shadowClone = false;
                    bomb = false;
                    onlyOnce = false;


                }
                else if (resetHolder == 3)
                {
                    resetHolder = 0;
                    thrust = false;
                    teleportBehind = false;
                    shadowClone = true;
                    bomb = false;
                    onlyOnce = false;


                }
                else if (resetHolder == 4)
                {
                    resetHolder = 0;
                    thrust = false;
                    teleportBehind = false;
                    shadowClone = false;
                    bomb = true;
                    onlyOnce = false;


                }
            }

            //when the boss has no more health
            if (GetComponent<Health>().currentHealth <= 0)
            {
                BossDies();
            }

        }







        if (leap == true)
        {
            leapMove();
        }

        if (thrust == true)
        {
            thrustAttack();
        }

        if (teleportBehind == true)
        {
            teleportBehindAttack();
        }

        if (shadowClone == true)
        {
            shadowCloneAttack();
        }

        if (bomb == true)
        {
            bombAttack();
        }


    }








    // reset with a leap in the air and does an ability while its still in the air 
    void leapMove()
    {
        if (leapOnlyOnce == false)
        {
            rb.AddForce(Vector2.up * leapHeight, ForceMode2D.Impulse);
            if (transform.position.x < player.transform.position.x)
            {
                rb.linearVelocity = new Vector2(leapHorizontalForce, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(-leapHorizontalForce, rb.linearVelocity.y);
            }

            leapOnlyOnce = true;
        }
    }











    //Thrust ability where it jumps directly out towards the player from the air
    void thrustAttack()
    {
        if (onlyOnce == false)
        {

            moveDirection = (player.transform.position - transform.position).normalized * thrustSpeed;
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y);
            Invoke("resetBoss", thrustDurration);
            onlyOnce = true;
        }

    }







    //teleports behind the player and dashes towards them
    void teleportBehindAttack()
    {
        if (onlyOnce == false)
        {

            if (transform.position.x < player.transform.position.x)
            {
                transform.position = new Vector2(player.transform.position.x + behindPlayerDist, player.transform.position.y);
                behindRight = true;

            }
            else
            {
                transform.position = new Vector2(player.transform.position.x - behindPlayerDist, player.transform.position.y);
                behindLeft = true;
            }

            onlyOnce = true;
        }
        if (behindLeft == true)
        {
            rb.AddForce(Vector2.right * jabFromBehindSpeed, ForceMode2D.Impulse);
            behindLeft = false;
            behindRight = false;
            Invoke("resetBoss", jabFromBehindDurration);
        }
        if (behindRight == true)
        {
            rb.AddForce(Vector2.left * jabFromBehindSpeed, ForceMode2D.Impulse);
            behindLeft = false;
            behindRight = false;
            Invoke("resetBoss", jabFromBehindDurration);
        }


    }









    //spawns shadowclone and they both thrust towards the player similarly to the first ability 
    void shadowCloneAttack()
    {
        if (onlyOnce == false)
        {
            onlyOnce = true;
            cloneHolder = Random.Range(1, 3);
            cloneResetHolder = Time.time + 5;

        }

        if (cloneHolder != 0)
        {
            if (cloneHolder == 1)
            {
                transform.position = new Vector2(player.transform.position.x - cloneHorizontalDist, player.transform.position.y + cloneHeight);
                rb.AddForce(Vector2.down * cloneDownForce, ForceMode2D.Impulse);
                Instantiate(clone, new Vector2(player.transform.position.x + cloneHorizontalDist, player.transform.position.y + cloneHeight), transform.rotation);
                cloneHolder = 0;

            }
            else if (cloneHolder == 2)
            {
                transform.position = new Vector2(player.transform.position.x + cloneHorizontalDist, player.transform.position.y + cloneHeight);
                rb.AddForce(Vector2.down * cloneDownForce, ForceMode2D.Impulse);
                Instantiate(clone, new Vector2(player.transform.position.x - cloneHorizontalDist, player.transform.position.y + cloneHeight), transform.rotation);
                cloneHolder = 0;


            }





        }
        if (cloneResetHolder != 0)
        {
            if (cloneResetHolder < Time.time)
            {
                resetBoss();
                transform.position = new Vector2(player.transform.position.x + behindPlayerDist, transform.position.y);
                Destroy(GameObject.FindGameObjectWithTag("SBoss5Minion"));
            }
        }




    }









    //dashes over the player and drops a bomb while over them
    void bombAttack()
    {

        if (transform.position.x < player.transform.position.x + 1)
        {
            if (transform.position.x > player.transform.position.x - 1)
            {
                if (onlyOneBomb == false)
                {
                    Instantiate(bombGameObject, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation);
                    onlyOneBomb = true;
                }
            }
        }
        if (onlyOnce == false)
        {
            onlyOnce = true;

            if (transform.position.x < player.transform.position.x)
            {

                //boss is on left
                rightDashHolder = Time.time + bombDashTime;
                rb.AddForce(Vector2.up * bombUpForce, ForceMode2D.Impulse);
                rb.AddForce(Vector2.right * bombDashSpeed, ForceMode2D.Impulse);

            }
            else
            {

                //boss is on right
                leftDashHolder = Time.time + bombDashTime;
                rb.AddForce(Vector2.up * bombUpForce, ForceMode2D.Impulse);
                rb.AddForce(Vector2.left * bombDashSpeed, ForceMode2D.Impulse);
            }
        }

        if (rightDashHolder != 0)
        {
            if (rightDashHolder < Time.time)
            {
                //boss dashes right but is on the left
                rb.linearVelocity = new Vector2(0, 0);
                rb.AddForce(Vector2.down * bombDownForce, ForceMode2D.Impulse);
                Invoke("resetBoss", bombResetTime);
                leftDashHolder = 0;
                rightDashHolder = 0;
            }
        }

        if (leftDashHolder != 0)
        {
            if (leftDashHolder < Time.time)
            {
                //boss dashes left but is on the right
                rb.linearVelocity = new Vector2(0, 0);
                rb.AddForce(Vector2.down * bombDownForce, ForceMode2D.Impulse);
                Invoke("resetBoss", bombResetTime);
                leftDashHolder = 0;
                rightDashHolder = 0;
            }
        }


    }









    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (shadowClone == true)
        {
            if (collision.gameObject.tag == "ground")
            {
                if (transform.position.x < player.transform.position.x)
                {
                    rb.linearVelocity = new Vector2(cloneHorizontalForce, rb.linearVelocity.y);
                }
                else
                {
                    rb.linearVelocity = new Vector2(-cloneHorizontalForce, rb.linearVelocity.y);
                }
            }




            if (collision.gameObject.tag == "SBoss5Minion")
            {
                Destroy(GameObject.FindGameObjectWithTag("SBoss5Minion"));
                resetBoss();
            }
        }

    }








    //reset
    void resetBoss()
    {
        onlyOneBomb = false;
        cloneHolder = 0;
        rb.linearVelocity = new Vector2(0, 0);
        leap = true;
        thrust = false;
        teleportBehind = false;
        shadowClone = false;
        bomb = false;
        leapOnlyOnce = false;
        holder = Time.time + resetTime;

        CancelInvoke();
    }








    //heath related
    void BossDies()
    {
        //play die animation

        //destroy
        Destroy(gameObject, 0);

    }






}
