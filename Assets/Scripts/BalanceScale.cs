using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceScale : MonoBehaviour
{
    [SerializeField] Transform leftGoal, rightGoal;
    [SerializeField] int motorForce;
    [SerializeField] float torqueForce;
    
    GameManager gm;
    float epsilon = 0.01f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (transform.rotation.z < -epsilon) {
            rb.AddTorque(torqueForce);
        }
        else if (transform.rotation.z > epsilon) { 
            rb.AddTorque(-torqueForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Goal") {
            if (collision.name == leftGoal.name)
                gm.RightScore++;
            else
                gm.LeftScore++;
            gm.ResetLevel();
        }
    }

    public void Reset() {
        transform.rotation = Quaternion.identity;
        rb.angularVelocity = 0;
    }
}
