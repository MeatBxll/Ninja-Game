using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    private Rigidbody2D rigidbodyComponent;

    public float jumpMultiplier;

    public int availableJumps;

    private int currentJump;

    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float jumpDelayTime;
    private float startJumpDelayTime;

    private bool usingJump;



    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    


    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        startJumpDelayTime = 0.1f;
        jumpDelayTime = startJumpDelayTime;
    }

    // Update is called once per frame
    void Update()
    {


        
        // Checks if their is any jumps available and allows the platyer to jump if their is
        if (currentJump < availableJumps && usingJump == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidbodyComponent.velocity = Vector2.up * jumpMultiplier;
                usingJump = true;
                currentJump++;
            }
        }

        // Adds a force on the way down from the jump
        // Can tap the jump to not go as high or hold it to reach the peak

        if (rigidbodyComponent.velocity.y < 0) {
            rigidbodyComponent.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rigidbodyComponent.velocity.y > 0 && !Input.GetButton("Jump")) {
            rigidbodyComponent.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        
        // checks if the player is touching the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);


        // resets the amount of jumps when grounded
        if (currentJump > 0)
        {
            if (isGrounded && usingJump == false)
            {
                currentJump = 0;
            }
        }

        // adds a small delay when jumping to prevent the player from getting an extra jump when first jumping
        if(jumpDelayTime <= 0 && usingJump == true)
        {
            usingJump = false;
            jumpDelayTime = startJumpDelayTime;
        }
        else if(usingJump == true)
        {
            jumpDelayTime -= Time.deltaTime;
        }



    }
}
        



