using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 2f;
    [SerializeField] KeyCode left, right, jump;

    public KeyCode JumpKey { get { return jump; } }

    private bool wantToJump = false;
    private float horizontalMove = 0f;
    CharacterController2D controller;
    Animator anim;

    private void Start() {
        controller = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalMove = (Input.GetKey(left)) ? -1 : 0;
        horizontalMove += (Input.GetKey(right)) ? 1 : 0;

        if (Input.GetKeyDown(jump))
        {
            wantToJump = true;
        }
        if (horizontalMove != 0)
            anim.SetBool("Walk", true);
        else
            anim.SetBool("Walk", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime * runSpeed, wantToJump);
        wantToJump = false;
    }
}
