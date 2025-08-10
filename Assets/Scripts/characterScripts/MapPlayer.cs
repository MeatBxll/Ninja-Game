using System;
using UnityEngine;

public class MapPlayer : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [NonSerialized] public gameController gameController;
    private Rigidbody2D rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector3(inputX, inputY, 0f) * moveSpeed;

    }
}
