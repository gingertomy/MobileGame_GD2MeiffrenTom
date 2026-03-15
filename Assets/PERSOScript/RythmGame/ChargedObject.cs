using System;
using UnityEngine;

public class ChargedObject : MonoBehaviour
{
    private bool _playerIsCharged = false;
    [SerializeField] private PlayerSlide _playerSlide;
    public static event Action OnPickupCharged;
    [SerializeField] private Animator Animator;

    private void OnEnable()
    {
        if (_playerSlide != null)
            _playerSlide.OnStateChanged += ChargeChange;
    }

    private void OnDisable()
    {
        if (_playerSlide != null)
            _playerSlide.OnStateChanged -= ChargeChange;
    }

    private void ChargeChange(bool status)
    {
        _playerIsCharged = status;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
   
            if (_playerIsCharged || (_playerSlide != null && _playerSlide.isCharged))
            {
               
                OnPickupCharged?.Invoke();
                Animator.SetTrigger("Scored");
                gameObject.SetActive(false);
                
            }
        }
    }
}