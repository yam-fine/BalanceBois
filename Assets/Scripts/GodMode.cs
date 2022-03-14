using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodMode : MonoBehaviour, Pickupable {

    public void OnPickup(Player player) {
        player.GodMode();
        Destroy(gameObject);
    }
}
