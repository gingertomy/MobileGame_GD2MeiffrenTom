using System;
using Unity.VisualScripting;
using UnityEngine;

public class Panier : MonoBehaviour
{

    public event Action fruitInside;
    public event Action trashInside;
    [SerializeField] private Animator _animatorfruit;
    [SerializeField] private Animator _animatorTrash;

    [SerializeField] private AudioEventDispatcher _audioEventDispatcher;
    [SerializeField] private AudioType _points;
    [SerializeField] private AudioType _negative;


    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Fruit"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Fruit récupéré");
            _animatorfruit.SetTrigger("Idle");
            _animatorfruit.SetTrigger("Scored");
            _audioEventDispatcher.PlayAudio(_points);
            fruitInside?.Invoke();

        }
        if (collision.CompareTag("Trash"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Trash récupéré");
            _animatorTrash.SetTrigger("Idle");
            _animatorTrash.SetTrigger("Scored");
            _audioEventDispatcher.PlayAudio(_negative);
            trashInside?.Invoke();

        }
    }
}

    
