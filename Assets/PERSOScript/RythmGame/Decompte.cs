using System.Collections;
using UnityEngine;
using TMPro;

public class Decompte : MonoBehaviour
{
    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private TMP_Text _texte;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _button;

    void Start()
    {
        Time.timeScale = 0f;
        _button.SetActive(false);
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {

      

        yield return new WaitForSecondsRealtime(0.25f);
        for (int i = 3; i > 0; i--)
        {
            _texte.text = i.ToString();
            _animator.SetTrigger("Decompte");

            yield return new WaitForSecondsRealtime(1f);
        }
        _button.SetActive(true);
        Time.timeScale = 1f;
        _AudioSource.Play();
    }
}