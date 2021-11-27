using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        hj = GetComponent<HingeJoint2D>();
        jm = hj.motor;
    }

    private void Update() {
        //if (Mathf.Abs(transform.rotation.z) <= resetValue)
        //    Reset();

        if (transform.rotation.z < 0)
             jm.motorSpeed = -motorForce;
        else if (transform.rotation.z > 0)
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
        }
    }

    private void Reset() {
        transform.rotation = Quaternion.identity;
    }
}
