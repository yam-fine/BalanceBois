using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceScale : MonoBehaviour
{
    [SerializeField] Transform leftGoal, rightGoal;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Goal") {
            if (collision.name == leftGoal.name)
                gm.LeftScore++;
            else
                gm.RightScore++;
        }
    }
}
