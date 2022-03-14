using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillYourself : MonoBehaviour
{
    [SerializeField] float timeToDie = 1;

    float time = 0;

    // Update is called once per frame
    void Update()
    {
        if (time > timeToDie)
            Destroy(gameObject);
        time += Time.deltaTime;
    }
}
