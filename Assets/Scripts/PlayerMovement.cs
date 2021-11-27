using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 2f;

    private bool jump = false;
    private float horizontalMove = 0f;
    CharacterController2D controller;

    private void Start() {
        controller = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        horizontalMove=Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedTime , false, jump);
        jump = false;
    }
}
