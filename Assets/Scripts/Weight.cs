using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight : MonoBehaviour, Pickupable
{
    [SerializeField] float weight, jump;

    public void OnPickup(Player player) {
        player.GetComponent<Rigidbody2D>().mass += weight;
        player.GetComponent<CharacterController2D>().JumpForce += jump;
        Destroy(gameObject);
    }
}
