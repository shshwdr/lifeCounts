using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontalMove;
    Rigidbody2D rb;
    Vector2 movement;
    public float speed = 5;
    CharacterController2D controller;
    bool isJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        movement = horizontalMove * Vector2.right * speed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
    }
    private void FixedUpdate()
    {
        controller.Move(horizontalMove* speed, false, isJump);
        isJump = false;
        //if (movement.magnitude > 0.01)
        //{

        //    rb.MovePosition(rb.position + movement * Time.deltaTime);
        //}
    }
}
