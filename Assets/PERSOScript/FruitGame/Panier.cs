using System;
using Unity.VisualScripting;
using UnityEngine;

public class Panier : MonoBehaviour
{

    public event Action fruitInside;
    public event Action trashInside;
    [SerializeField] private Animator _animator;

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Fruit"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Fruit récupéré");
            _animator.SetTrigger("Idle");
            _animator.SetTrigger("Scored");
            fruitInside?.Invoke();

        }
        if (collision.CompareTag("Trash"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Trash récupéré");
            trashInside?.Invoke();

        }
    }
}

    
