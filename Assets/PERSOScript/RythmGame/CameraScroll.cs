using UnityEngine;

public class CameraScroll : MonoBehaviour
{

    public float verticalSpeed = 5f;   
    public float horizontalSpeed = 5f;
    [SerializeField] private ScoreLifeManagerFly _scoreLifeManagerFly;
    [SerializeField] private StopZone _stopZone;
    private int _lastScoreApplied = -1;

    private void OnEnable()
    {
        if (_scoreLifeManagerFly != null)
            _scoreLifeManagerFly.scoreReached += IncreaseSpeed;

        if (_stopZone != null)
            _stopZone.StopCam += StopCamera;
    }

    private void OnDisable()
    {
        if (_scoreLifeManagerFly != null)
            _scoreLifeManagerFly.scoreReached -= IncreaseSpeed;

        if ( _stopZone != null)
            _stopZone.StopCam -= StopCamera;
    }
    void Update()
    {
       
        Vector3 movement = new Vector3(horizontalSpeed, verticalSpeed, 0);

        
        transform.position += movement * Time.deltaTime;
    }

    private void IncreaseSpeed(int score)
    {
        
        if (score >= 25 && _lastScoreApplied < 25)
        {
            horizontalSpeed = 8f;
            _lastScoreApplied = 25;
        }
        else if (score >= 20 && _lastScoreApplied < 20)
        {
            horizontalSpeed = 7f;
            _lastScoreApplied = 20;
        }
        else if (score >= 15 && _lastScoreApplied < 15)
        {
            horizontalSpeed = 6f;
            _lastScoreApplied = 15;
        }
        else if (score >= 10 && _lastScoreApplied < 10)
        {
            horizontalSpeed = 5f;
            _lastScoreApplied = 10;
        }
        else if (score >= 5 && _lastScoreApplied < 5)
        {
            horizontalSpeed = 4f;
            _lastScoreApplied = 5;
        }

        
    }
    private void StopCamera()
    {
        horizontalSpeed = 0f;
        verticalSpeed = 0f;
    }
}