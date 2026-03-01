using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class InputSlidePlayer : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 0.5f;
    [SerializeField] private float _maxX = 5f;          

    private Camera _mainCamera;

    private void OnEnable()
    {
        
        EnhancedTouchSupport.Enable();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Touch.activeTouches.Count == 0) return;

        var touch = Touch.activeTouches[0];

        
        if (touch.phase == TouchPhase.Moved)
        {
            
            Vector2 touchDelta = touch.delta;

            
            float moveX = touchDelta.x * _sensitivity * Time.deltaTime;

            Vector3 newPosition = transform.position;
            newPosition.x += moveX;

            
            newPosition.x = Mathf.Clamp(newPosition.x, -_maxX, _maxX);

            transform.position = newPosition;
        }
    }
}