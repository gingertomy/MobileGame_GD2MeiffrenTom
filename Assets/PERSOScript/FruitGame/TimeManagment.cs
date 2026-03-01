using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManagment : MonoBehaviour
{
    private float _timeStepDuration = 0.8f;
    public event Action OnTimePassed;
    private int _lastScoreApplied = -1;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private AudioSource _music;  
    IEnumerator SpendingTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeStepDuration);
            OnTimePassed?.Invoke();
        }
    }

    private void OnEnable()
    {
        _scoreManager.scoreReached += AccelerateTime;
        
    }

    private void OnDisable()
    {
        _scoreManager.scoreReached -= AccelerateTime;
        
    }

   
       private void AccelerateTime(int score)
       {
        
        if (score >= 80 && _lastScoreApplied < 80)
        {
            _lastScoreApplied = 80;
            SetTime(0.1f);
            SetMusic(1.5f);
        }
        else if (score >= 70 && _lastScoreApplied < 70)
        {
            _lastScoreApplied = 70;
            SetTime(0.2f);
            SetMusic(1.4f);
        }
        else if (score >= 60 && _lastScoreApplied < 60)
        {
            _lastScoreApplied = 60;
            SetTime(0.4f);
            SetMusic(1.3f);
        }
        else if (score >= 40 && _lastScoreApplied < 40)
        {
            _lastScoreApplied = 40;
            SetTime(0.5f);
            SetMusic(1.2f);
        }
        else if (score >= 20 && _lastScoreApplied < 20)
        {
            _lastScoreApplied = 20;
            SetTime(0.7f);
            SetMusic(1.1f);
        }
    }
    private void SetTime(float newTime)
    {
        Debug.Log($"Time accelerated to {newTime} seconds");
        _timeStepDuration = newTime;
        StopTime();
        StartTime();
    }
    private void SetMusic(float newSpeed)
    {
        
        _music.pitch = newSpeed;
        
    }


    private void Start()
    {
        StartTime();

    }
    private void StartTime()
    {
        StartCoroutine(SpendingTime());
    }

    private void StopTime()
    {
        StopAllCoroutines();
    }



}


