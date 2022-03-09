using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int currentScene;
    [SerializeField] GameObject levelSelectPopup;
    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }  

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentScene + 1);
    }

    public void LevelSelectionPopup()
    {
        levelSelectPopup.SetActive(true);
    }
    
    public void LoadLobbyScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(currentScene);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }
   
    
}
