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

    private void OnTriggerStay2D(Collider2D collision) // "Stay" est plus sûr que "Enter" si on relâche en étant déjà dessus
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Double vérification : l'event OU la variable en direct
            if (_playerIsCharged || (_playerSlide != null && _playerSlide.isCharged))
            {
                Debug.Log("Impact réussi avec charge !");

                OnPickupCharged?.Invoke();
                Animator.SetTrigger("Scored");
                gameObject.SetActive(false);
                
            }
        }
    }
}