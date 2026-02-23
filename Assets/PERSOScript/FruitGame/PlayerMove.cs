using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform[] m_transforms;
    [SerializeField] private InputSwipePlayer m_inputSwipePlayer;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite _player;
    [SerializeField] private Sprite _leftSprite;
    [SerializeField] private Sprite _rightSprite;
    [SerializeField] private Animator _playerAnimator;

    public event Action fruitPickup;
    public event Action trashPickup;


    private int m_currentIndex = 0;
    private int m_moveSpeed = 1;
    private bool m_isCarryingTrash = false;


    private void OnEnable()
    {
        m_inputSwipePlayer.OnMoveLeft += MoveToPrevPosition;
        m_inputSwipePlayer.OnMoveRight += MoveToNextPosition;
    }

    private void OnDisable()
    {
        m_inputSwipePlayer.OnMoveLeft -= MoveToPrevPosition;
        m_inputSwipePlayer.OnMoveRight -= MoveToNextPosition;
    }

    private void Start()
    {
        m_currentIndex = 1;
        transform.position = m_transforms[m_currentIndex].position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void MoveToNextPosition()
    {
       
        m_currentIndex += m_moveSpeed;
        m_currentIndex = Mathf.Clamp(m_currentIndex, 0, m_transforms.Length - 1);
        UpdatePosition();

    }

    public void MoveToPrevPosition()
    {
        m_currentIndex -= m_moveSpeed;
        m_currentIndex = Mathf.Clamp(m_currentIndex, 0, m_transforms.Length - 1);
        UpdatePosition();
    }

    public void MoveToDirection(int direction) //direction = -1 OU 1
    {
        m_currentIndex = m_currentIndex + m_moveSpeed * direction;
        m_currentIndex = Mathf.Clamp(m_currentIndex, 0, m_transforms.Length - 1);
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        if (m_currentIndex == 0)
        {
            spriteRenderer.sprite = _leftSprite;
            _playerAnimator.enabled = false;
        }
        if (m_currentIndex == 2)
        {
            spriteRenderer.sprite = _rightSprite;
            _playerAnimator.enabled = false;
        }
        if (m_currentIndex == 1)
        {
            spriteRenderer.sprite = _player;
            _playerAnimator.enabled = true;
        }
        transform.position = m_transforms[m_currentIndex].position;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Fruit"))
        {
            Destroy(collision.gameObject); 
            Debug.Log("Fruit récupéré");
            fruitPickup?.Invoke();
        }
        if (collision.CompareTag("Trash"))
        {
            Destroy(collision.gameObject); 
            Debug.Log("Trash récupéré");
            trashPickup?.Invoke();

        }
    }
}
