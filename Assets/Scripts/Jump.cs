using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour, Pickupable {
    [SerializeField] float jumpForce;

    public void OnPickup(Player player) {
        player.GetComponent<CharacterController2D>().JumpForce *= jumpForce;
        Destroy(gameObject);
    }
}
