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
    [SerializeField] private Animator _trashAnimator;

    [SerializeField] private AudioEventDispatcher _audioEventDispatcher;
    [SerializeField] private AudioType _moveAudioType;
    [SerializeField] private AudioType _getTrashAudioType;
    [SerializeField] private AudioType _getFruitAudioType;

    public event Action fruitPickup;
    public event Action trashPickup;


    private int m_currentIndex = 0;
    private int m_moveSpeed = 1;
  


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
        _audioEventDispatcher.PlayAudio(_moveAudioType);
        UpdatePosition();

    }

    public void MoveToPrevPosition()
    {
        m_currentIndex -= m_moveSpeed;
        m_currentIndex = Mathf.Clamp(m_currentIndex, 0, m_transforms.Length - 1);
        _audioEventDispatcher.PlayAudio(_moveAudioType);
        UpdatePosition();
    }

    public void MoveToDirection(int direction) 
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
            _trashAnimator.SetTrigger("Idle");
            _trashAnimator.SetTrigger("Scored");
            _audioEventDispatcher.PlayAudio(_getFruitAudioType);
            fruitPickup?.Invoke();
        }
        if (collision.CompareTag("Trash"))
        {
            Destroy(collision.gameObject); 
            Debug.Log("Trash récupéré");
            _audioEventDispatcher.PlayAudio(_getTrashAudioType);
            trashPickup?.Invoke();

        }
    }
}
