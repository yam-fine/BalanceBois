using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acrobat : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maxSpawnRate, minSpawnRate;
    [SerializeField] SpriteRenderer balanceScale;
    [SerializeField] float acrobatHeight;
    [SerializeField] List<GameObject> buffs;

    Vector2 leftTarget, rightTarget;
    bool goingRight = false;
    float timeToSpawn, currTimeToSpawn;

    private void Start() {
        leftTarget = new Vector2(-(balanceScale.bounds.size.x / 2), balanceScale.transform.position.y + acrobatHeight);
        rightTarget = leftTarget;
        rightTarget.x *= -1;
        currTimeToSpawn = Random.Range(minSpawnRate, maxSpawnRate);
    }

    void Update()
    {
        Movement();

        SpawnBuffs();
    }

    void SpawnBuffs() {
        if (timeToSpawn < currTimeToSpawn) {
            timeToSpawn += Time.deltaTime;
        }
        else {
            timeToSpawn = 0;
            Instantiate(buffs[Random.Range(0, buffs.Count)], transform.position, Quaternion.identity);
            currTimeToSpawn = Random.Range(minSpawnRate, maxSpawnRate);
        }
    }

    void Movement() {
        if (goingRight) {
            transform.position = Vector2.MoveTowards(transform.position, rightTarget, Time.deltaTime * speed);
            if ((Vector2)transform.position == rightTarget)
                goingRight = false;
        }
        else {
            transform.position = Vector2.MoveTowards(transform.position, leftTarget, Time.deltaTime * speed);
            if ((Vector2)transform.position == leftTarget)
                goingRight = true;
        }
    }
}
