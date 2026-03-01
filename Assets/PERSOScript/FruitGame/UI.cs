using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private AudioEventDispatcher _audioEventDispatcher;
    [SerializeField] private AudioType _clickAudioType;

    public void OnClick()
    {
        _audioEventDispatcher.PlayAudio(_clickAudioType);
    }
}
