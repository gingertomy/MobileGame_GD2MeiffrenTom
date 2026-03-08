using System;
using UnityEngine;

public class Podium : MonoBehaviour
{

    public event Action OnPodiumReached;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPodiumReached?.Invoke();
        }
    }
}
