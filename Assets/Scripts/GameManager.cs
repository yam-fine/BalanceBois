using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text uiScoreLeft, uiScoreRight, countdownTime;
    [SerializeField] int gameTime = 60;
    [SerializeField] float gameOverDelay;

    int leftScore, rightScore;
    Player[] players;
    List<Rigidbody2D> rbs;
    BalanceScale bs;
    float currTime, defaultWeight;

    public int LeftScore { get { return leftScore; } set { leftScore = value; uiScoreLeft.text = leftScore.ToString(); } }
    public int RightScore { get { return rightScore; } set { rightScore = value; uiScoreRight.text = rightScore.ToString(); } }

    static GameManager instance;
    public static GameManager Instance {
        get {
            if (!instance) {
                instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindObjectsOfType<Player>();
        bs = GameObject.FindObjectOfType<BalanceScale>();
        currTime = gameTime;
        defaultWeight = players[0].GetComponent<Rigidbody2D>().mass;
        rbs = new List<Rigidbody2D>();
        foreach (Player p in players) {
            rbs.Add(p.GetComponent<Rigidbody2D>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        CountdownTimer();
    }

    void CountdownTimer() {
        currTime -= Time.deltaTime;
        countdownTime.text = Mathf.CeilToInt(currTime).ToString();
        if (currTime <= 0) {
            currTime = 0;
            GameOver();
        }
    }

    void GameOver() {
        StartCoroutine(timer());
    }

    IEnumerator timer() {
        float time = 0;
        while (time < gameOverDelay) {
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetLevel() {
        foreach (Player p in players)
            p.Reset();
        foreach (Rigidbody2D rb in rbs)
            rb.mass = defaultWeight;
        bs.Reset();
        foreach (GameObject pickup in GameObject.FindGameObjectsWithTag("Pickupable"))
            Destroy(pickup);
    }
}
