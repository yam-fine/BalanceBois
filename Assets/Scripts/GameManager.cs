using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text uiScoreLeft, uiScoreRight;

    int leftScore, rightScore;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("SampleScene");
    }
}
