using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text uiScoreLeft, uiScoreRight, countdownTime;
    [SerializeField] int gameTime = 60;

    int leftScore, rightScore;
    Player[] players;
    BalanceScale bs;
    float currTime;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("SampleScene");

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

    }

    public void ResetLevel() {
        foreach (Player p in players) {
            p.Reset();
        }
        bs.Reset();
    }
}
