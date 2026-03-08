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


    [SerializeField] private float _gravityScale = 0.5f; 

    [SerializeField] private Rigidbody2D _rb;

    public event Action BalloonHit;
    public event Action SpikeHit;

    private void OnEnable() => EnhancedTouchSupport.Enable();

    private void Start()
    {

        _rb.gravityScale = _gravityScale;
        _rb.linearDamping = _drag;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        _rb.freezeRotation = true;
    }

    private void Update()
    {
        if (Touch.activeTouches.Count > 0)
        {
            var touch = Touch.activeTouches[0];

            if (touch.phase == TouchPhase.Moved)
            {

                Vector2 forceDirection = touch.delta;


                _rb.AddForce(forceDirection * _pushForce, ForceMode2D.Impulse);
            }
        }

        if (_rb.linearVelocity.magnitude > _maxSpeed)
        {
            _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity, _maxSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Balloon"))
        {
            collision.gameObject.SetActive(false);
           BalloonHit?.Invoke();
        }
        else if (collision.CompareTag("Spike"))
        {
            collision.gameObject.SetActive(false);
            SpikeHit?.Invoke();
        }

    }
}