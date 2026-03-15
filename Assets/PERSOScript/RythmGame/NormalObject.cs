using System;
using Unity.VisualScripting;
using UnityEngine;

public class NormalObject : MonoBehaviour
{

    public static event Action OnPickupNormal;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sprite;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
                OnPickupNormal?.Invoke();
            spriteRenderer.sprite = sprite;
            gameObject.SetActive(false);
            
               

        }
    }
}
