using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class PlayerWind : MonoBehaviour
{

    [SerializeField] private float _pushForce = 1.2f;
    [SerializeField] private float _maxSpeed = 8f;
    [SerializeField] private float _drag = 1.5f;
    [SerializeField] private float _gravity = 0.5f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _spriteRenderer; 
    [SerializeField] private Sprite _spriteLeft;
    [SerializeField] private Sprite _spriteRight;

    public event Action<int> BalloonHit;
    public event Action SpikeHit;

    private void OnEnable() => EnhancedTouchSupport.Enable();

    private void Start()
    {
        _rb.gravityScale = _gravity;
        _rb.linearDamping = _drag;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        _rb.freezeRotation = true;


        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Touch.activeTouches.Count > 0)
        {
            var touch = Touch.activeTouches[0];

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 forceDirection = touch.delta;


                UpdateSpriteDirection(forceDirection.x);

                _rb.AddForce(forceDirection * _pushForce, ForceMode2D.Impulse);
            }
        }

        if (_rb.linearVelocity.magnitude > _maxSpeed)
        {
            _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity, _maxSpeed);
        }
    }

    private void UpdateSpriteDirection(float deltaX)
    {

        if (Mathf.Abs(deltaX) < 0.1f) return;

        if (deltaX > 0)
        {
            _spriteRenderer.sprite = _spriteRight;

        }
        else if (deltaX < 0)
        {
            _spriteRenderer.sprite = _spriteLeft;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Balloon"))
        {
            collision.gameObject.SetActive(false);
            BalloonHit?.Invoke(1);
        }
        else if (collision.CompareTag("Balloon2"))
        {
            collision.gameObject.SetActive(false);
            BalloonHit?.Invoke(2);
        }
        else if (collision.CompareTag("Balloon3"))
        {
            collision.gameObject.SetActive(false);
            BalloonHit?.Invoke(3);
        }
        else if (collision.CompareTag("Spike"))
        {
            collision.gameObject.SetActive(false);
            SpikeHit?.Invoke();
        }
    }
}