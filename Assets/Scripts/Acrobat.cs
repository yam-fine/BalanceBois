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
    [SerializeField] GameObject specialBuff;

    Vector2 leftTarget, rightTarget;
    bool goingRight = false;
    float timeToSpawn, currTimeToSpawn;
    GameManager gm;
    int specialSpawns = 2;
    float firstSpecialBuff, secondSpecialBuff, balanceFactor = 5;

    private void Start() {
        leftTarget = new Vector2(-(balanceScale.bounds.size.x / 2) + balanceFactor, balanceScale.transform.position.y + acrobatHeight);
        rightTarget = leftTarget;
        rightTarget.x *= -1;
        if (Random.Range(0, 2) == 1) {
            goingRight = true;
            Flip();
        }
        currTimeToSpawn = Random.Range(minSpawnRate, maxSpawnRate);
        gm = GameManager.Instance;
        firstSpecialBuff = gm.GameTime * 2/3; // decimal calculations can't be made before runtime
        secondSpecialBuff = gm.GameTime * 1/3;
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

        if ((gm.CurrentTime <= firstSpecialBuff && specialSpawns == 2) || 
            (gm.CurrentTime <= secondSpecialBuff && specialSpawns == 1)) {
            specialSpawns--;
            Instantiate(specialBuff, transform.position, Quaternion.identity);
        }
    }

    void Movement() {
        if (goingRight) {
            transform.position = Vector2.MoveTowards(transform.position, rightTarget, Time.deltaTime * speed);
            if ((Vector2)transform.position == rightTarget) {
                goingRight = false;
                Flip();
            }
        }
        else {
            transform.position = Vector2.MoveTowards(transform.position, leftTarget, Time.deltaTime * speed);
            if ((Vector2)transform.position == leftTarget) {
                goingRight = true;
                Flip();
            }
        }
    }

    void Flip() {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
