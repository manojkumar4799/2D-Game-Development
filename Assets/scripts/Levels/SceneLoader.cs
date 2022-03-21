using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int currentScene;
    [SerializeField] GameObject levelSelectPopup;
    [SerializeField] GameObject LevelCompleteUI;
    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }  

    public void LoadNextScene()
    {
        SoundManager.Instance.PlaySound(Sound.StartLevel);
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentScene + 1);
        
    }

    public void LevelCompletePopup()
    {
        Time.timeScale = 0f;
        LevelCompleteUI.SetActive(true);
        LevelManager.Instance().MarkLevelStatus();
        SoundManager.Instance.PlaySound(Sound.levelComplete);
       
    }

    public void LevelSelectionPopup()
    {
        SoundManager.Instance.PlaySound(Sound.ButtonClick);
        levelSelectPopup.SetActive(true);
    }
    
    public void LoadLobbyScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(currentScene);
        Time.timeScale = 1f;
        SoundManager.Instance.BackGroundMusic(Sound.GameMusic);
    }

    public void Quit()
    {
        Application.Quit();
    }
   
    
}
