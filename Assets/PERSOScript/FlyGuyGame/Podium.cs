using System;
using UnityEngine;

public class Podium : MonoBehaviour
{

    public event Action OnPodiumReached;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            OnPodiumReached?.Invoke();
        }
    }
}
