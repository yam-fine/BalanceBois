using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    public void Reset() {
        transform.position = spawnPoint.position;
    }
}
