using System;
using UnityEngine;

namespace UnityEngine.SceneManagement
{
    public class CollishionHandeler : MonoBehaviour
    {
        [SerializeField]
        float deleyTime = 2f;
        private void OnCollisionEnter(Collision other)
        {
            switch (other.gameObject.tag)
            {
                case "Frendly":
                    break;
                case "Finish":
                    DelayBeforeNextLvl();
                    LoadNextLevel();
                    break;
                default:
                    StartCrashSequence();
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

        void StartCrashSequence()
        {
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadLevel", 1.5f);
        }
        void DelayBeforeNextLvl()
        {
            GetComponent<Movement>().enabled = false;
            Invoke("LoadNextLevel", deleyTime);
        }
    }
}


