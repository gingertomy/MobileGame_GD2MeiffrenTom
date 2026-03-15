using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] AudioSource _AudioSource;
    public void LoadNewLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);

    }

    private void Start()
    {
        Time.timeScale = 1f;
    }   
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        LoadNewLevel(0);

    }

    public void LoadLevel1()
    {
        LoadNewLevel(1);

    }
    public void LoadLevel2()
    {
        LoadNewLevel(2);


    }

    public void LoadLevel3()
    {
        LoadNewLevel(3);


    }
    public void Pause()
    {
        Time.timeScale = 0f;
        if (_AudioSource != null)
        {
            _AudioSource.Pause();
        }
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        if (_AudioSource != null)
        {
            _AudioSource.Play();
        }
    }

}