using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {START, IN_PROGRESS, DEFEAT}
public class GameManager : MonoBehaviour
{
    static public GameManager Instance { get; private set; }
   
    public delegate void DefeatDelegate();
    public DefeatDelegate defeatDelegate;

    public bool invincible = false;

    private int _score = 0;
    private int _chances = 3;
    public int _startingCountScore = 0;
    public int _currentScore = 0;

    private GameState _gameState = GameState.START;
    private Hud_Manager _hudManager;

    void Awake()
    {
        Instance = this;
        _gameState = GameState.IN_PROGRESS;
    }

    private void Start()
    {
        _hudManager = Hud_Manager.Instance;
    }

    //Score
    public void UpdateScore(int score)
    {
        if (score > 0)
        {
            _score += score;
        }

        if (score < 0)
        {
            if (_score > 0)
                _score += score;

            if (_score < 0)
                _score = 0;

            RemoveChance();
            StartCounting();
        }

        _hudManager.UpdateScoringText(_score);
    }

    //Chance
    public void AddChance()
    {
        if (_chances < 3)
            _chances++;

        _hudManager.UpdateChanceImage(_chances);
    }

    public void RemoveChance()
    {
        if (invincible)
            return;

        if (_gameState == GameState.DEFEAT)
            return;

        if (_chances > 0)
            _chances--;

        _hudManager.UpdateChanceImage(_chances);
    }

    //Defeat
    private void Defeat()
    {
        _gameState = GameState.DEFEAT;
        StartCoroutine(DefeatSreen());
        print("Defeat");
    }

    private void StartCounting()
    {
        _startingCountScore = _score;
    }

    IEnumerator DefeatSreen()
    {
        WaitForSeconds wfs = new WaitForSeconds(1.7f);

        yield return wfs;

        Hud_Manager.Instance.DefeatScreen();
    }

    private void FixedUpdate()
    {
        if (_chances == 0 && _gameState != GameState.DEFEAT)
        {
            Defeat();

            if (defeatDelegate != null)
            {
                defeatDelegate();
            }
        }

        if(_score - _startingCountScore == 100)
        {
            AddChance();
            _startingCountScore = _score;
        }

        Block_Manager.Instance.ChangeColor();
        _currentScore = _score - _startingCountScore;
    }
}
