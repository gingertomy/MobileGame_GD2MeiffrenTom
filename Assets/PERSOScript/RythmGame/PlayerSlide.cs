using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using UnityEngine.Rendering.Universal;
using System.Collections;
using System;

public class PlayerSlide : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 0.5f;
    [SerializeField] private float _maxX = 5f;
    [SerializeField] private float _minSwipeDistance = 50f;
    [SerializeField] private float _maxChargeDistance = 300f;

    [SerializeField] private Light2D _chargeLight;
    [SerializeField] private float _minIntensity = 0.5f;
    [SerializeField] private float _maxIntensity = 5f;
    [SerializeField] private float _minRadius = 1f;
    [SerializeField] private float _maxRadius = 4f;

    private Vector2 _touchStartPos;
    public float currentCharge = 0f;
    private bool _isCharging = false;

    public bool isCharged = false;
    public event Action<bool> OnStateChanged;

    private void OnEnable() => EnhancedTouchSupport.Enable();

    private void Start()
    {
        if (_chargeLight != null) UpdateLight(0);
    }

    private void Update()
    {
        if (Touch.activeTouches.Count == 0)
        {
            if (_isCharging) ReleaseAttack();
            return;
        }

        var touch = Touch.activeTouches[0];

        if (touch.phase == TouchPhase.Began)
        {
            _touchStartPos = touch.screenPosition;
        }

        if (touch.phase == TouchPhase.Moved)
        {
            MoveLateral(touch.delta.x);

            Vector2 currentTouchPos = touch.screenPosition;
            float swipeVertical = _touchStartPos.y - currentTouchPos.y;

            if (swipeVertical > _minSwipeDistance)
            {
                _isCharging = true;
                currentCharge = Mathf.Clamp01(swipeVertical / _maxChargeDistance);
                UpdateLight(currentCharge);
            }
        }

        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            if (_isCharging) ReleaseAttack();
        }
    }

    private void UpdateLight(float chargeValue)
    {
        if (_chargeLight == null) return;
        _chargeLight.intensity = Mathf.Lerp(_minIntensity, _maxIntensity, chargeValue);
        _chargeLight.pointLightOuterRadius = Mathf.Lerp(_minRadius, _maxRadius, chargeValue);
        _chargeLight.color = Color.Lerp(Color.white, Color.red, chargeValue);
    }

    private void ReleaseAttack()
    {
        _isCharging = false; 

        if (currentCharge >= 0.9f)
        {
            StopAllCoroutines(); 
            StartCoroutine(ChargedDuration());
        }
        else
        {
            currentCharge = 0f; 
            UpdateLight(0);
        }
    }

    IEnumerator ChargedDuration()
    {
        isCharged = true;
        OnStateChanged?.Invoke(true);
        if (_chargeLight != null) _chargeLight.color = Color.yellow;

        yield return new WaitForSeconds(0.3f);

        OnStateChanged?.Invoke(false);
        isCharged = false;
        currentCharge = 0f; 

        if (_chargeLight != null)
        {
            _chargeLight.color = Color.white;
            UpdateLight(0);
        }
    }

    private void MoveLateral(float deltaX)
    {
        float moveX = deltaX * _sensitivity * Time.deltaTime;
        Vector3 newPos = transform.position;
        newPos.x = Mathf.Clamp(newPos.x + moveX, -_maxX, _maxX);
        transform.position = newPos;
    }
}