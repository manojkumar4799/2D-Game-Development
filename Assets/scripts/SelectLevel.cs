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
 
        [SerializeField] string sceneName;

        public void LoadSelectedScene()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
