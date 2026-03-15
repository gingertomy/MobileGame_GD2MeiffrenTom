using UnityEngine;

public class DataPersist : MonoBehaviour
{
    [SerializeField] private Score _score; 
    private void Awake()
    {
        
        DontDestroyOnLoad(gameObject);
    }
}