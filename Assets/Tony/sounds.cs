using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sounds : MonoBehaviour
{
    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Vector2 lastPos;
    private Vector2 currentPos;

    public AudioSource footSteps;
    void Start()
    {
        
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (isGrounded)
        {
            currentPos = transform.position;
            if (currentPos == lastPos)
            {
                footSteps.mute = true;
            }
            else
            {
                footSteps.mute = false;
            }
            lastPos = currentPos;
        }
        else
        {
            footSteps.mute = true;
        }
    }
}
