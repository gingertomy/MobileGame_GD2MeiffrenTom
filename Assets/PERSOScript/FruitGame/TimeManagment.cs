using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManagment : MonoBehaviour
{
    private float _timeStepDuration = 0.8f;
    public event Action OnTimePassed;
    [SerializeField] private ScoreManager _scoreManager;
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
        if (score == 20)
        {
            _timeStepDuration = 0.7f;
            StopTime();
            StartTime();
        }
        else if (score == 40)
        {
            _timeStepDuration = 0.5f;
            StopTime();
            StartTime();
        }
        else if (score == 60)
        {
            _timeStepDuration = 0.4f;
            StopTime();
            StartTime();
        }
        else if (score == 70)
        {
            _timeStepDuration = 0.2f;
            StopTime();
            StartTime();
        }
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
