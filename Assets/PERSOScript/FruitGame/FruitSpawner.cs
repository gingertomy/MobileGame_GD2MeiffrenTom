using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private TimeManagment _timeManagment;
    [SerializeField] private FruitMovement[] _fallingLines;

    [SerializeField] private int _spawnTimer = 0;
    [SerializeField] private int _spawnInterval = 3;

    [SerializeField] private GameObject[] _fruitPrefabs;
    [SerializeField] private GameObject[] _trashPrefabs;
    


    [SerializeField] private float _trashChance = 30f;

    private void OnEnable()
    {
        _timeManagment.OnTimePassed += TimeGestion;
    }

    private void OnDisable()
    {
        _timeManagment.OnTimePassed -= TimeGestion;
    }

    private int random()
    {
        return Random.Range(0, _fallingLines.Length);
    }



    private void TimeGestion()
    {

        _spawnTimer++;

        if (_spawnTimer >= _spawnInterval)
        {
            _spawnTimer = 0;


            GameObject prefabToSpawn = null;
            float roll = Random.Range(0f, 100f);

            if (roll < _trashChance && _trashPrefabs.Length > 0)
            {
                prefabToSpawn = _trashPrefabs[Random.Range(0, _trashPrefabs.Length)];
            }
            else if (_fruitPrefabs.Length > 0)
            {
                prefabToSpawn = _fruitPrefabs[Random.Range(0, _fruitPrefabs.Length)];
            }

            if (prefabToSpawn != null)
            {
                GameObject newObj = Instantiate(prefabToSpawn);

          
                int lineIndex = random();

               
                FruitBehavior behavior = newObj.AddComponent<FruitBehavior>();

                
                behavior.Setup(_fallingLines[lineIndex].GetPath(), _timeManagment);
            }
        }
    }
}

