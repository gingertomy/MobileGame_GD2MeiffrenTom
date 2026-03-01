using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using UnityEngine.InputSystem.EnhancedTouch;


public class InputSwipePlayer : MonoBehaviour
{
    public event Action OnMoveLeft;
    public event Action OnMoveRight;

    [SerializeField] private float _tapDuration = 1.0f;
    private float _tapTimer = 0.0f;
    private bool _isTouching = false;
    private float width = 0.0f;
    private float height = 0.0f;

    private Vector2 startPosition;
    private Vector2 endPosition;




    private void Start()
    {
        width = Screen.width;
        height = Screen.height;




    }
    

private void OnEnable()
{
    EnhancedTouchSupport.Enable();
}

private void OnDisable()
{
    EnhancedTouchSupport.Disable();
}

private void OnSwipe()
    {
        Vector2 delta = endPosition - startPosition;
        delta = delta.normalized;
        float dot = Vector2.Dot(delta, Vector2.right);

        if (Mathf.Abs(dot) > 0.7f)
        {
            if (dot < 0)
            {
                MoveLeft();
            }
            else
            {
                MoveRight();
            }
        }
    }

    private void Update()
    {
        if (Touch.activeTouches.Count == 0)
        {
            return;
        }
        var touch = Touch.activeTouches[0];

        if (touch.phase == TouchPhase.Began)
        {
            startPosition = touch.screenPosition;

        }
        if (touch.phase == TouchPhase.Ended)
        {
            endPosition = touch.screenPosition;
            OnSwipe();


        }
    }


    public void MoveLeft()
    {
        OnMoveLeft?.Invoke();
    }

    public void MoveRight()
    {
        OnMoveRight?.Invoke();

    }
}
