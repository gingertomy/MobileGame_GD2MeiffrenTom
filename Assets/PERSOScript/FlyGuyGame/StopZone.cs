using System;
using UnityEngine;

public class StopZone : MonoBehaviour
{

    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private bool _isFlyGuyGame = false;
    [SerializeField] private AudioEventDispatcher _EventDispatcher;
    [SerializeField] private AudioType _victory;

    public event Action StopCam;
    public event Action Stars;


    private void Start()
    {
        if (_victoryScreen != null)
        _victoryScreen.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopCam?.Invoke();
            if (!_isFlyGuyGame )
            {
                Time.timeScale = 0f;
            }
            if (_audioSource != null)
                _audioSource.Stop();
            if (_victoryScreen != null)
                _victoryScreen.SetActive(true);
            _EventDispatcher.PlayAudio(_victory);
        }

    }
}
