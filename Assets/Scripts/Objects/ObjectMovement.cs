using System;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _transforms;
    private int _index = -1;
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private GameObject _ObjectFalling;
    [SerializeField] private AudioEventDispatcher _audioEventDispatcher;
    [SerializeField] private AudioType _ObjectMovementAudioType;
    [SerializeField] private AudioType _ObjectDestructionAudioType;
    public void Init(GameObject NewObject)
    {
        _ObjectFalling = NewObject;
        MoveObject();

    }
    private void OnEnable()
    {
        _timeManager.OnTimePassed += MoveObject;
    }

    private void OnDisable()
    {
        _timeManager.OnTimePassed -= MoveObject;
    }

    private void MoveObject()
    {
        if (_ObjectFalling == null)
        {
            Debug.LogWarning("Object is null, cannot move");
            return;
        }
        Debug.LogWarning($"Object moved to position {_index + 1}");
        _index++;
        if (_index < _transforms.Length)
        {
           
            _ObjectFalling.transform.position = _transforms[_index].position;
            _audioEventDispatcher.PlayAudio(_ObjectMovementAudioType);
        }
        else
        {
           
                Destroy(_ObjectFalling);
           
            Debug.LogWarning("Object Destroyed");
            _audioEventDispatcher.PlayAudio(_ObjectDestructionAudioType);
            _index = -1;
        }

    }
}