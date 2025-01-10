using System;
using UnityEngine;

namespace UnityEngine.SceneManagement
{
    public class CollishionHandeler : MonoBehaviour
    {
        
        
        [SerializeField] 
        AudioClip crashAudio;
        
        [SerializeField] 
        AudioClip successAudio;
        
        AudioSource crashAudioSource;
        AudioSource successAudioSource;

        bool isContrilable = true;

        
        private void Start()
        {
            crashAudioSource = GetComponent<AudioSource>();
            successAudioSource = GetComponent<AudioSource>();
        }
        private void OnCollisionEnter(Collision other)
        {
            if (!isContrilable)
                return;
            
            switch (other.gameObject.tag)
            {
                case "Frendly":
                    break;
                case "Finish":
                    DelayBeforeNextLvl(1f);
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
            isContrilable = false;
            successAudioSource.Stop();
            crashAudioSource.PlayOneShot(crashAudio);
            StopControl(isContrilable);
            Invoke("ReloadLevel", 0.4f);
        }
        void DelayBeforeNextLvl(float deleyTime)
        {
            isContrilable = false;
            crashAudioSource.Stop();
            successAudioSource.PlayOneShot(successAudio);
            StopControl(isContrilable);
            Invoke("LoadNextLevel", deleyTime);
        }

        void StopControl(bool canControll)
        {
            GetComponent<Movement>().enabled = canControll;
        }
    }
}


