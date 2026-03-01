using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] private Panier[] _paniers;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private Score _score;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private Animator _textAnimator;

    [SerializeField] private AudioEventDispatcher _audioEventDispatcher;
    [SerializeField] private AudioType _acceleration;

    private bool _boost20;
    private bool _boost40;
    private bool _boost60;
    private bool _boost70;
    private bool _boost80;
    
        

    public int _actualscore;

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
       _playerMove.fruitPickup += RemoveScore;
    }

    private void OnDisable()
    {
        foreach (var panier in _paniers)
        {
            if (panier != null)
                panier.fruitInside -= AddScore;
        }
        _playerMove.fruitPickup -= RemoveScore;
    }

    private void AddScore()
    {
        _actualscore++;

       
        if (_actualscore >= 20 && !_boost20)
        {
            TriggerAcceleration(20); 
            _boost20 = true;
        }
        else if (_actualscore >= 40 && !_boost40)
        {
            TriggerAcceleration(40); 
            _boost40 = true;
        }
        else if (_actualscore >= 60 && !_boost60)
        {
            TriggerAcceleration(60); 
            _boost60 = true;
        }
        else if (_actualscore >= 70 && !_boost70)
        {
            TriggerAcceleration(70); 
            _boost70 = true;
        }
        else if (_actualscore >= 80 && !_boost80)
        {
            TriggerAcceleration(80); 
            _boost80 = true;
        }

        UpdateScore();
        UpdateBestScore();
    }

    private void TriggerAcceleration(int score)
    {
        _audioEventDispatcher.PlayAudio(_acceleration);
        _textAnimator.SetTrigger("Idle");
        _textAnimator.SetTrigger("Acceleration");
        scoreReached?.Invoke(score);
    }
    private void RemoveScore()
    {
        if (_actualscore > 0)
        {
            _actualscore -= 3;
            _actualscore = Mathf.Max(_actualscore, 0);
            UpdateScore();
        }
    }


    private void UpdateScore()
    {
        _scoreText.text = $"Score : {_actualscore.ToString()}";
    }

    private void UpdateBestScore()
    {
        
        if (_actualscore > _score._bestScore)
        {
            _score._bestScore = _actualscore;
        }
        _bestScoreText.text = $"High-Score : {_score._bestScore}";
    }
}

