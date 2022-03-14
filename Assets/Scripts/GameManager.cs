using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text uiScoreLeft, uiScoreRight, countdownTime;
    [SerializeField] int gameTime = 60;
    [SerializeField] float gameOverDelay;
    [SerializeField] List<GameObject> players;
    [SerializeField] GameObject acrobat, clock;
    [SerializeField] GameObject endingImage;
    [SerializeField] Sprite leftDef, leftRes, leftExit, rightDef, rightRes, rightExit, tieDef, tieRes, tieExit;
    [SerializeField] GameObject endGameAudio;

    int leftScore, rightScore;
    Player[] playerScript;
    List<Rigidbody2D> rbs;
    List<CharacterController2D> cc2;
    BalanceScale bs;
    float currTime, defaultWeight, defaultJump;
    bool pauseTime = true;
    Image ei;
    enum Won { Left, Right, Tie};
    Won whoWon;

    public int LeftScore { get { return leftScore; } set { leftScore = value; uiScoreLeft.text = leftScore.ToString(); } }
    public int RightScore { get { return rightScore; } set { rightScore = value; uiScoreRight.text = rightScore.ToString(); } }
    public float CurrentTime { get { return currTime; } }
    public int GameTime { get { return gameTime; } }

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
        bs = GameObject.FindObjectOfType<BalanceScale>();
        rbs = new List<Rigidbody2D>();
        cc2 = new List<CharacterController2D>();
        ei = endingImage.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (!pauseTime)
            CountdownTimer();
    }

    public void GameStart() {
        foreach(GameObject p in players) {
            p.SetActive(true);
        }
        playerScript = GameObject.FindObjectsOfType<Player>();
        foreach (Player p in playerScript) {
            rbs.Add(p.GetComponent<Rigidbody2D>());
            cc2.Add(p.GetComponent<CharacterController2D>());
        }
        defaultWeight = rbs[0].mass;
        defaultJump = cc2[0].JumpForce;
        uiScoreLeft.gameObject.SetActive(true);
        uiScoreRight.gameObject.SetActive(true);
        clock.gameObject.SetActive(true);
        acrobat.gameObject.SetActive(true);
        currTime = gameTime;

        pauseTime = false;
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
        if (leftScore > rightScore) {
            whoWon = Won.Left;
            ei.sprite = leftDef;
        }
        else if (rightScore > leftScore) {
            whoWon = Won.Right;
            ei.sprite = rightDef;
        }
        else {
            whoWon = Won.Tie;
            ei.sprite = tieDef;
        }
        endingImage.SetActive(true);
        foreach (GameObject pickup in GameObject.FindGameObjectsWithTag("Pickupable"))
            Destroy(pickup);
        foreach (GameObject p in players)
            Destroy(p);
        acrobat.SetActive(false);
        bs.Reset();
        endGameAudio.SetActive(true);
    }

    public void ResetLevel() {
        foreach (Player p in playerScript)
            p.Reset();
        foreach (Rigidbody2D rb in rbs)
            rb.mass = defaultWeight;
        foreach (CharacterController2D cc in cc2)
            cc.JumpForce = defaultJump;
        foreach (GameObject pickup in GameObject.FindGameObjectsWithTag("Pickupable"))
            Destroy(pickup);
        bs.Reset();
    }

    public void Restart() {
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit() {
        SceneManager.LoadScene("MainMenu");
    }

    public void HoverRestart() {
        if (whoWon == Won.Left)
            ei.sprite = leftRes;
        else if (whoWon == Won.Right)
            ei.sprite = rightRes;
        else
            ei.sprite = tieRes;
    }

    public void HoverExit() {
        Debug.Log("lol");
        if (whoWon == Won.Left)
            ei.sprite = leftExit;
        else if (whoWon == Won.Right)
            ei.sprite = rightExit;
        else
            ei.sprite = tieExit;
    }

    public void NoHover() {
        if (leftScore > rightScore) {
            ei.sprite = leftDef;
        }
        else if (rightScore > leftScore) {
            ei.sprite = rightDef;
        }
        else {
            ei.sprite = tieDef;
        }
    }
}
