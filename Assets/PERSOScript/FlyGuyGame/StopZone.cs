using System;
using UnityEngine;

public class StopZone : MonoBehaviour
{

    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private AudioSource _audioSource;

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
            StopCam();
            Time.timeScale = 0f; 
            if (_audioSource != null)
                _audioSource.Stop();
            if (_victoryScreen != null)
                _victoryScreen.SetActive(true);
        }

    }
}
