using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private Panier[] _paniers; // ← tableau maintenant
    [SerializeField] private int _lives = 3;
    [SerializeField] private GameObject[] _lifeIcons;

    private void Start()
    {
        _lives = _lifeIcons.Length;
        UpdateLives();
    }

    private void OnEnable()
    {
        foreach (var panier in _paniers)
        {
            if (panier != null)
                panier.trashInside += LoseLife;
        }
    }

    private void OnDisable()
    {
        foreach (var panier in _paniers)
        {
            if (panier != null)
                panier.trashInside -= LoseLife;
        }
    }

    private void LoseLife()
    {
        if (_lives > 0)
        {
            _lives--;
            UpdateLives();
        }

        if (_lives <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    private void UpdateLives()
    {
        for (int i = 0; i < _lifeIcons.Length; i++)
        {
            _lifeIcons[i].SetActive(i < _lives);
        }
    }
}