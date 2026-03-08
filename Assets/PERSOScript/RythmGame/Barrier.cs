using UnityEngine;
using UnityEngine.UI; 

public class Barrier : MonoBehaviour
{
    [SerializeField] private Slider _Slider;
    [SerializeField] private float _degatscharged = 0.1f;
    [SerializeField] private float _degats = 0.1f;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        _Slider.value += 1f;
        _gameOverScreen.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision détectée avec : " + collision.gameObject.name);

        if (collision.GetComponent<NormalObject>())
        {
            BaisserSlider();
            
        }
        else if (collision.GetComponent<ChargedObject>())
        {
            BaisserSliderCharged();
            
        }
    }

    private void BaisserSlider()
    {
        if (_Slider != null)
        {

            _Slider.value -= _degats;
            _animator.SetTrigger("Missed");


            if (_Slider.value <= 0)
            {
                _gameOverScreen.SetActive(true);
                _audioSource.Stop();
                Time.timeScale = 0; 
            }
        }
    }

    private void BaisserSliderCharged()
    {
        if(_Slider != null)
        {

            _Slider.value -= _degatscharged;
            _animator.SetTrigger("Missed");


            if (_Slider.value <= 0)
            {
               _gameOverScreen.SetActive(true);
                _audioSource.Stop();
                Time.timeScale = 0;
            }
        }
    }

}