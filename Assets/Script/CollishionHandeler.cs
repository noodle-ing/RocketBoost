using System;
using UnityEngine;

namespace UnityEngine.SceneManagement
{
    public class CollishionHandeler : MonoBehaviour
    {
        [SerializeField]
        private void OnCollisionEnter(Collision other)
        {
            switch (other.gameObject.tag)
            {
                case "Frendly":
                    break;
                case "Finish":
                    DelayBeforeNextLvl(0.4f);
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
            Invoke("ReloadLevel", 0.4f);
        }
        void DelayBeforeNextLvl(float deleyTime)
        {
            GetComponent<Movement>().enabled = false;
            Invoke("LoadNextLevel", deleyTime);
        }
    }
}


