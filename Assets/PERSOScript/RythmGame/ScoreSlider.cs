using UnityEngine;
using UnityEngine.UI;

public class ScoreSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _charged = 0.1f;
    [SerializeField] private float _normal = 0.05f;

    [SerializeField] private StopZone stopZone;
    [SerializeField] private GameObject[] _stars; 

   

    private void OnEnable()
    {
        ChargedObject.OnPickupCharged += IncreaseScoreCharged;
        NormalObject.OnPickupNormal += IncreaseScoreNormal;
        stopZone.Stars += UpdateStars;
    }

    private void OnDisable()
    {
        ChargedObject.OnPickupCharged -= IncreaseScoreCharged;
        NormalObject.OnPickupNormal -= IncreaseScoreNormal;
    }

    private void IncreaseScoreCharged()
    {
        _slider.value += _charged;
        UpdateStars();
    }

    private void IncreaseScoreNormal()
    {
        _slider.value += _normal;
        UpdateStars();
    }

    private void UpdateStars()
    {
        float val = _slider.value;

        for (int i = 0; i < _stars.Length; i++)
        {

            float threshold = (i + 1) * 0.2f;

            if (val >= threshold)
            {
                _stars[i].SetActive(true);
            }
            else
            {
                _stars[i].SetActive(false);
            }
        }
    }
}