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
                    SceneManager.LoadScene(levelName);
                    break;

                case LevelStatus.Completed:
                    SceneManager.LoadScene(levelName);
                    break;

            }
            
        }
    }
}
