using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    [SerializeField] float fallMult = 1.5f, lowMult = 1;

    Rigidbody2D rb;
    KeyCode jump;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jump = GetComponent<PlayerMovement>().JumpKey;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMult) * Time.deltaTime;
        else if (rb.velocity.y > 0 && !Input.GetKey(jump))
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowMult) * Time.deltaTime;
    }
}
