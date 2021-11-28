﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceScale : MonoBehaviour
{
    [SerializeField] Transform leftGoal, rightGoal;
    [SerializeField] int motorForce;
    [SerializeField] float resetValue;
    
    GameManager gm;
    HingeJoint2D hj;
    JointMotor2D jm;
    float epsilon = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        hj = GetComponent<HingeJoint2D>();
        jm = hj.motor;
    }

    private void Update() {
        if (transform.rotation.z < -epsilon)
            jm.motorSpeed = -motorForce;
        else if (transform.rotation.z > epsilon)
            jm.motorSpeed = motorForce;
        else
            jm.motorSpeed = 0;
        hj.motor = jm;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Goal") {
            if (collision.name == leftGoal.name)
                gm.LeftScore++;
            else
                gm.RightScore++;
            gm.ResetLevel();
        }
    }

    public void Reset() {
        transform.rotation = Quaternion.identity;
    }
}