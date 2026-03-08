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
            _lastScoreApplied = 80;
            verticalSpeed = 8f;

        }
        else if (score >= 20 && _lastScoreApplied < 20)
        {
            _lastScoreApplied = 70;
            verticalSpeed = 7f;

        }
        else if (score >= 15 && _lastScoreApplied < 15)
        {
            _lastScoreApplied = 60;
            verticalSpeed = 6f;

        }
        else if (score >= 10 && _lastScoreApplied < 10)
        {
            _lastScoreApplied = 40;
            verticalSpeed = 5f;

        }
        else if (score >= 5 && _lastScoreApplied < 5)
        {
            _lastScoreApplied = 20;
            verticalSpeed = 4f;
        }
    }

    private void StopCamera()
    {
        horizontalSpeed = 0f;
        verticalSpeed = 0f;
    }
}