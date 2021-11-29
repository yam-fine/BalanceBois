using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    
    public void Reset() {
        transform.position = spawnPoint.position;
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Pickupable") {
            collision.GetComponent<Pickupable>().OnPickup(this);
        }
    }
}
