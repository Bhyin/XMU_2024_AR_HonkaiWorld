using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class ScoreCounter : MonoBehaviour
{

    public TextMeshProUGUI valkyriesScore;
    public TextMeshProUGUI monstersScore;
    public TextMeshProUGUI winInfo;

    public GameObject scorePanel;
    public GameObject gameStartPanel;
    public GameObject gameOverPanel;


    int valkyriesScoreCount = 0;
    int monstersScoreCount = 0;

    public static bool gameStart = false;

    // Start is called before the first frame update
    void Start()
    {
        valkyriesScoreCount = 0;
        monstersScoreCount = 0;
        SetValkyriesScore();
        SetMonstersScore();

        scorePanel.SetActive(false);
        gameStartPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStart) return;

        if (valkyriesScoreCount + monstersScoreCount >= 8)
        {
            GameOver();
            if (valkyriesScoreCount > monstersScoreCount)
            {
                winInfo.text = "Valkyries Win!";
            }
            else if (valkyriesScoreCount < monstersScoreCount)
            {
                winInfo.text = "Monsters Win!";
            }
            else
            {
                winInfo.text = "Score Draw!";
            }
        }
    }

    public void StartGame()
    {
        gameStart = true;
        gameStartPanel.SetActive(false);
        scorePanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        gameStart = false;
        gameStartPanel.SetActive(false);
        scorePanel.SetActive(false);
        gameOverPanel.SetActive(true);

        valkyriesScoreCount = 0;
        monstersScoreCount = 0;
        SetValkyriesScore();
        SetMonstersScore();
    }

    public void GameOverConfirm()
    {
        gameOverPanel.SetActive(false);
        scorePanel.SetActive(false);
        gameStartPanel.SetActive(true);
    }

    void SetValkyriesScore()
    {
        string[] comps = valkyriesScore.text.Split('\n');
        valkyriesScore.text = comps[0] + '\n' + valkyriesScoreCount.ToString();
    }


    void SetMonstersScore()
    {
        string[] comps = monstersScore.text.Split('\n');
        monstersScore.text = comps[0] + '\n' + monstersScoreCount.ToString();
    }


    public void AddValkyriesScore(int score)
    {
        valkyriesScoreCount += score;
        SetValkyriesScore();
    }

    public void AddMonstersScore(int score)
    {
        monstersScoreCount += score;
        SetMonstersScore();
    }

}
