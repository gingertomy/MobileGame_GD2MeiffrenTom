using UnityEngine;

public class FruitBehavior : MonoBehaviour
{
    private Transform[] _path; 
    private int _currentIndex = 0;
    private TimeManagment _timeManager;
    

    
    public void Setup(Transform[] points, TimeManagment manager)
    {
        _path = points;
        _timeManager = manager;
        _timeManager.OnTimePassed += MoveForward;

        transform.position = _path[0].position;
    }

    private void MoveForward()
    {
        _currentIndex++;

        if (_currentIndex < _path.Length)
        {
            transform.position = _path[_currentIndex].position;
        }
        else
        {
            Die(); 
        }
    }

    public void Die()
    {
        if (_timeManager != null) _timeManager.OnTimePassed -= MoveForward;
        Destroy(gameObject);
    }

    private void OnDestroy() 
    {
        if (_timeManager != null) _timeManager.OnTimePassed -= MoveForward;
    }
}