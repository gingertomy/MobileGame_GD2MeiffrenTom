using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] private Panier[] _paniers;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private Score _score;
    
    private int _actualscore;

    public event Action<int> scoreReached;


    private void Start()
    {
        UpdateBestScore();
        UpdateScore();
    }
    private void OnEnable()
    {
        foreach (var panier in _paniers)
        {
            if (panier != null)
                panier.fruitInside += AddScore;
        }
    }

    private void OnDisable()
    {
        foreach (var panier in _paniers)
        {
            if (panier != null)
                panier.fruitInside -= AddScore;
        }
    }

    private void AddScore()
    {
        
        _actualscore++;
        

        if (_actualscore >= 20)
        {
            scoreReached?.Invoke(20);
        }

        else if (_actualscore >= 40)
        {
            scoreReached?.Invoke(40);
        }

        else if (_actualscore >= 60)
        {
            scoreReached?.Invoke(60);
        }

        else if (_actualscore >= 70)
        {
            scoreReached?.Invoke(70);
        }

        else if (_actualscore >= 80)
        {
            scoreReached?.Invoke(80);
        }

        UpdateScore();
        UpdateBestScore();
    }

    private void UpdateScore()
    {
        _scoreText.text = $"Score : {_actualscore.ToString()}";
    }

    private void UpdateBestScore()
    {
        _bestScoreText.text = $"High-Score : {_actualscore.ToString()}";
        if (_actualscore > _score._bestScore)
        {
            _score._bestScore = _actualscore;
        }
    }
}

