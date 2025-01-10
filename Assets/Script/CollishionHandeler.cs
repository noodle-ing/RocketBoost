using System;
using UnityEngine;

namespace UnityEngine.SceneManagement
{
    public class CollishionHandeler : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            switch (other.gameObject.tag)
            {
                case "Frendly":
                    break;
                case "Finish":
                    LoadNextLevel();
                    break;
                default:
                    ReloadLevel();
                    break;
            }
        }

        void ReloadLevel()
        {
            int curentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(curentScene);
        }

        void LoadNextLevel()
        {
            int curentScene = SceneManager.GetActiveScene().buildIndex;
            int nextScene = curentScene + 1;
            if (nextScene == SceneManager.sceneCountInBuildSettings) 
            {
                nextScene = 0;
            }
            SceneManager.LoadScene(nextScene);
        }
    }
}


