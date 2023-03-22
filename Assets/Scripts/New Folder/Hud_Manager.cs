using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class Hud_Manager : MonoBehaviour
{
    static public Hud_Manager Instance { get; private set; }

    [Header("Score")]
    public TextMeshProUGUI scoreText;

    [Header("Edges")]
    public Image leftEdge;
    public Image rightEdge;

    [Header("Chance")]
    public Image[] chanceImage;
    public GameObject chanceBoard;

    [Header("Defeat")]
    public GameObject defeatScreen;
    public TextMeshProUGUI defeatScore;

    private int _score;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.defeatDelegate += Defeat;
    }

    //Score
    public void UpdateScoringText(int score)
    {
        string _scoreText = "";

        if (score < 10)
            _scoreText = "00" + score.ToString();
        else if (score < 100)
            _scoreText = "0" + score.ToString();
        else
            _scoreText = score.ToString();

        scoreText.text = _scoreText;
        defeatScore.text = _scoreText;
    }

    //Chance
    public void UpdateChanceImage(int _id)
    {
        for (int i = 0; i < 3; i++)
        {
            chanceImage[i].enabled = false;

            if (i < _id)
                chanceImage[i].enabled = true;
        }
    }

    //Edge
    public void UpdateEdgesColor(Color[] colors)
    {
        leftEdge.color = colors[0];
        rightEdge.color = colors[1];
    }

    private void Defeat()
    {
        scoreText.enabled = false;
        chanceBoard.SetActive(false);
    }

    public void DefeatScreen()
    {
        defeatScreen.SetActive(true);
    }
}
