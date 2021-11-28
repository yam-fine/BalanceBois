using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 2f;
    [SerializeField] KeyCode left, right, jump;

    private bool jumping = false;
    private float horizontalMove = 0f;
    CharacterController2D controller;

    private void Start() {
        controller = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        horizontalMove = (Input.GetKeyDown(left)) ? -1 : 0; //Input.GetAxisRaw("Horizontal") * runSpeed;
        horizontalMove += (Input.GetKeyDown(right)) ? 1 : 0;

        if (Input.GetKeyDown(jump))
        {
            jumping = true;
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime , false, jumping);
        jumping = false;
    }
}
