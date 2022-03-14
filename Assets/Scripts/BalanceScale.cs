using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BalanceScale : MonoBehaviour
{
    [SerializeField] Transform leftGoal, rightGoal;
    [SerializeField] float torqueForce;
    [SerializeField] CinemachineVirtualCamera vcam;
    [SerializeField] AudioSource bellDing, crowd;
    [SerializeField] List<AudioClip> croudCheers;
    [SerializeField] GameObject leftParticles, rightParticles;
    [SerializeField] Transform lpLoc, rpLoc;

    GameManager gm;
    float epsilon = 0.01f;
    Rigidbody2D rb;
    CinemachineBasicMultiChannelPerlin vcamMCP;
    float shakeTime = .3f;
    int amplitudeGain = 4;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        rb = GetComponent<Rigidbody2D>();
        vcamMCP = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update() {
        if (transform.rotation.z < -epsilon) {
            rb.AddTorque(torqueForce);
        }
        else if (transform.rotation.z > epsilon) { 
            rb.AddTorque(-torqueForce);
        }
        else {
            rb.angularVelocity = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Goal") {
            if (collision.name == leftGoal.name) {
                gm.LeftScore++;
                Instantiate(rightParticles, rpLoc.position, Quaternion.identity);
            }
            else { 
                gm.RightScore++;
                Instantiate(leftParticles, lpLoc.position, Quaternion.identity);
            }
            OnScore();
        }
    }

    void OnScore() {
        StartCoroutine(ScoreAudio());
        StartCoroutine(Shake());
        gm.ResetLevel();
    }

    IEnumerator ScoreAudio() {
        bellDing.Play();
        yield return new WaitForSeconds(.1f);
        crowd.clip = croudCheers[Random.Range(0, croudCheers.Count)];
        crowd.Play();
    }

    IEnumerator Shake() {
        float timePassed = 0;
        vcamMCP.m_AmplitudeGain = amplitudeGain;
        while (timePassed < shakeTime) {
            timePassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        vcamMCP.m_AmplitudeGain = 0;
    }

    public void Reset() {
        transform.rotation = Quaternion.identity;
        rb.angularVelocity = 0;
    }
}
