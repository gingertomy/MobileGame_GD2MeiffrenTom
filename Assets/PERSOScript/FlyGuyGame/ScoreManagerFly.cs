using TMPro;
using System;
using UnityEngine;

public class ScoreLifeManagerFly : MonoBehaviour
{

    public int _actualscore;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private Score _score;
    [SerializeField] private PlayerWind _playerWind;
    [SerializeField] private Sea _sea;
    [SerializeField] private Podium _podium;

    [SerializeField] private int _lives = 3;
    [SerializeField] private GameObject[] _lifeIcons;

    [SerializeField] private TMP_Text _finalScoreText;
    [SerializeField] private Canvas _gameOverCanvas;

    private bool _boost5;
    private bool _boost10;
    private bool _boost15;
    private bool _boost20;
    private bool _boost25;

    public event Action<int> scoreReached;



    private void OnEnable()
    {
        _playerWind.BalloonHit += AddScore;
        _playerWind.SpikeHit += LoseLife;
        _sea.GameOver += GameOver;
        if (_podium != null)
             _podium.OnPodiumReached += Won;
    }
    private void OnDisable()
    {
        _playerWind.BalloonHit -= AddScore;
        _playerWind.SpikeHit -= LoseLife;
        _sea.GameOver -= GameOver;
        if (_podium != null)
            _podium.OnPodiumReached -= Won;
    }
    private void AddScore()
    {
        _actualscore++;

        if (_actualscore >= 5 && !_boost5)
        {
            TriggerAcceleration(5);
            _boost5 = true;
        }
        else if (_actualscore >= 10 && !_boost10)
        {
            TriggerAcceleration(10);
            _boost10 = true;
        }
        else if (_actualscore >= 15 && !_boost15)
        {
            TriggerAcceleration(15);
            _boost15 = true;
        }
        else if (_actualscore >= 20 && !_boost20)
        {
            TriggerAcceleration(20);
            _boost20 = true;
        }
        else if (_actualscore >= 25 && !_boost25)
        {
            TriggerAcceleration(25);
            _boost25 = true;
        }

        UpdateScore();
        UpdateBestScore();
    }



    private void TriggerAcceleration(int score)
    {

        scoreReached?.Invoke(score);
    }


    private void UpdateScore()
    {
        _scoreText.text = $"Score : {_actualscore.ToString()}";
    }

    private void UpdateBestScore()
    {

        if (_actualscore > _score._bestScoreFly)
        {
            _score._bestScoreFly = _actualscore;
        }
        _bestScoreText.text = $"High-Score : {_score._bestScoreFly}";
    }


    private void Start()
    {
        Time.timeScale = 1;
        _lives = _lifeIcons.Length;
        UpdateBestScore();
        UpdateScore();
        _gameOverCanvas.gameObject.SetActive(false);
        UpdateLives();
    }

    

    private void LoseLife()
    {
        if (_lives > 0)
        {
            _lives--;
            

            UpdateLives();
        }

        if (_lives <= 0)
        {


           GameOver();

        }
    }

    private void UpdateLives()
    {
        for (int i = 0; i < _lifeIcons.Length; i++)
        {
            _lifeIcons[i].SetActive(i < _lives);
        }
    }
    private void GameOver()
    {
        Time.timeScale = 0;
        _gameOverCanvas.gameObject.SetActive(true);
        _finalScoreText.text = $"Score : {_actualscore.ToString()}";
        Debug.Log("Game Over");
    }

    private void Won()
    { 
        Debug.Log("You win !");
         Time.timeScale = 0;
    }
}


