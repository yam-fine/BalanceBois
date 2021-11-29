using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    
    public void Reset() {
        transform.position = spawnPoint.position;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log(collision.name);
        if (collision.tag == "Pickupable") {
            collision.GetComponent<Pickupable>().OnPickup(this);
        }
    }
}
