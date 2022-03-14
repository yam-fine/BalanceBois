using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] float knockbackForce, knockbackTime, forceTime;
    [SerializeField] bool isLeftTeam;
    [SerializeField] Light2D spotLight;
    [SerializeField] AudioSource ass;

    bool god = false;
    Rigidbody2D rb;
    Animator spotLightAnim;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        spotLightAnim = spotLight.GetComponent<Animator>();
    }

    public void Reset() {
        if (spawnPoint != null) { // this is a dumb line that only affects editor runs but unity bitches about it
            transform.position = spawnPoint.position;
            transform.rotation = Quaternion.identity;
            rb.velocity = Vector2.zero;
            god = false;
        }
    }

    private void OnBecameInvisible() {
        Reset();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Pickupable") {
            collision.GetComponent<Pickupable>().OnPickup(this);
        }
        if (god) {
            if ((isLeftTeam && collision.tag == "RightTeam") || (!isLeftTeam && collision.tag == "LeftTeam")) {
                Transform enemy = collision.transform.parent;
                Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                Vector2 difference = enemy.transform.position - transform.position;
                difference = new Vector2(difference.normalized.x * knockbackForce, difference.normalized.y);
                StartCoroutine(KnockBackTime(enemyRb, difference));
            }
        }
    }

    // time that enemy is pushed back
    IEnumerator KnockBackTime(Rigidbody2D enemy, Vector2 diff) {
        float time = 0;
        if (enemy != null) {
            while (time < forceTime) {
                enemy.AddForce(diff);
                time += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }

    // Time until buff fades
    IEnumerator KnockBack() {
        float time = 0;
        while (time < knockbackTime) {
            time += Time.deltaTime;
            if (time > knockbackTime / 2)
                spotLightAnim.enabled = true;
            yield return new WaitForEndOfFrame();
        }
        god = false;
        spotLightAnim.enabled = false;
        spotLight.enabled = false;
    }

    public void GodMode() {
        god = true;
        spotLight.enabled = true;
        StartCoroutine(KnockBack());
    }
}
