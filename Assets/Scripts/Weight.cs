using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight : MonoBehaviour, Pickupable
{
    [SerializeField] float weight;

    public void OnPickup(Player player) {
        player.GetComponent<Rigidbody2D>().mass += weight;
    }
}
