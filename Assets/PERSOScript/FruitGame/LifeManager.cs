using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private Panier[] _paniers; 
    [SerializeField] private int _lives = 3;
    [SerializeField] private GameObject[] _lifeIcons;
   
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private TMP_Text _finalScoreText;
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private AudioEventDispatcher _audioEventDispatcher;
    [SerializeField] private AudioType _life;
    [SerializeField] private AudioType _gameOver;
    [SerializeField] private AudioSource _music;


   


    private void Start()
    {
        Time.timeScale = 1;
        _lives = _lifeIcons.Length;
        _gameOverCanvas.gameObject.SetActive(false);
        UpdateLives();
    }

    private void OnEnable()
    {
        foreach (var panier in _paniers)
        {
            if (panier != null)
                panier.trashInside += LoseLife;
        }


    }

    private void OnDisable()
    {
        foreach (var panier in _paniers)
        {
            if (panier != null)
                panier.trashInside -= LoseLife;
        }
    }

    private void LoseLife()
    {
        if (_lives > 0)
        {
            _lives--;
            _audioEventDispatcher.PlayAudio(_life);
           
            UpdateLives();
        }

        if (_lives <= 0)
        {
            
         
            _audioEventDispatcher.PlayAudio(_gameOver);
            Time.timeScale = 0;
            _music.Stop();
            _gameOverCanvas.gameObject.SetActive(true);
            _finalScoreText.text = $"Score : {_scoreManager._actualscore.ToString()}";
            Debug.Log("Game Over");
            
        }
    }

    private void UpdateLives()
    {
        for (int i = 0; i < _lifeIcons.Length; i++)
        {
            _lifeIcons[i].SetActive(i < _lives);
        }
    }


}