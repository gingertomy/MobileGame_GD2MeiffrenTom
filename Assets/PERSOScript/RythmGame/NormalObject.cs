using System;
using UnityEngine;

public class NormalObject : MonoBehaviour
{

    public static event Action OnPickupNormal;
    [SerializeField] private Animator animator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
                OnPickupNormal?.Invoke();
            animator.SetTrigger("Scored");
            gameObject.SetActive(false);
            
                Debug.Log("Impact réussi sans charge !");

        }
    }
}
