using System;
using UnityEngine;

public class Sea : MonoBehaviour
{

    public event Action GameOver;  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           GameOver(); 
        }
    }
}
