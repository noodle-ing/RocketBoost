using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityEngine.SceneManagement
{
    public class CollishionHandeler : MonoBehaviour
    {
        [SerializeField] 
        AudioClip crashAudio;
        [SerializeField] 
        AudioClip successAudio;

        [SerializeField] 
        ParticleSystem successParticle;
        [SerializeField] 
        ParticleSystem crashParticle;
        
        AudioSource crashAudioSource;
        AudioSource successAudioSource;

        bool isContrilable = true;
        
        private void Start()
        {
            crashAudioSource = GetComponent<AudioSource>();
            successAudioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            RespondToDebugKeys();
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
            Debug.Log(curentScene);
        }
 
        void StartCrashSequence()
        {
            isContrilable = false;
            successAudioSource.Stop();
            crashAudioSource.PlayOneShot(crashAudio); //comment 
            crashParticle.Play();
            StopControl(isContrilable);
            Invoke("ReloadLevel", 0.4f);
        }
        void DelayBeforeNextLvl(float deleyTime)
        {
            isContrilable = false;
            crashAudioSource.Stop();
            successParticle.Play();
            successAudioSource.PlayOneShot(successAudio);
            StopControl(isContrilable);
            Invoke("LoadNextLevel", deleyTime);
        }

        void StopControl(bool canControll)
        {
            GetComponent<Movement>().enabled = canControll;
        }

         void RespondToDebugKeys()
        {   
            if (Keyboard.current.lKey.wasPressedThisFrame)
            {
                LoadNextLevel();
            }
            else if (Keyboard.current.cKey.wasPressedThisFrame)
            {
                isContrilable = !isContrilable;
            }
        }
    }
}


