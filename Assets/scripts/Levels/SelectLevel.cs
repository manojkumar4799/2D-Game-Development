using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneContoller
{
    [RequireComponent(typeof(Button))]
    public class SelectLevel : MonoBehaviour
    {
 
        [SerializeField] string levelName;

        public void LoadSelectedScene()
        {
            LevelStatus levelStatus = LevelManager.Instance().GetLevelStatus(levelName);
            switch (levelStatus)
            {
                case LevelStatus.Locked:
                    Debug.Log(levelName + " is loacked");
                    break;

                case LevelStatus.Unlocked:
                    SoundManager.Instance.PlaySound(Sound.ButtonClick);
                    SceneManager.LoadScene(levelName);
                    break;

                case LevelStatus.Completed:
                    SoundManager.Instance.PlaySound(Sound.ButtonClick);
                    SceneManager.LoadScene(levelName);
                    break;

            }
            
        }
    }
}
