using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class Movement : MonoBehaviour
{
    [SerializeField] 
    InputAction thrust;
    
    Rigidbody rb;
    
    [SerializeField] 
    float trustStrenght = 10f;

    [SerializeField]  
    InputAction rotation;

    [SerializeField] 
    float rotatevelocity =30f;
    
    [SerializeField] 
    ParticleSystem mainBoosterParticle;
    [FormerlySerializedAs("LeftBoosterParticle")] [SerializeField] 
    ParticleSystem leftBoosterParticle;
    [FormerlySerializedAs("RightBoosterParticle")] [SerializeField] 
    ParticleSystem rightBoosterParticle;

    [SerializeField] 
    AudioClip mainEngine;
    
    AudioSource flyAudio;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        flyAudio = GetComponent<AudioSource>(); 
    }
    
    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable(); 
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }
    
    private void ProcessThrust()
    {
       
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * trustStrenght * Time.fixedDeltaTime);
            if (!flyAudio.isPlaying)
            {
                flyAudio.PlayOneShot(mainEngine);
            }
            if (!mainBoosterParticle.isPlaying)
            {
                mainBoosterParticle.Play();
            }
        }
        else
        {
            flyAudio.Stop();
            mainBoosterParticle.Stop();
        }
    }
    
    
    void  ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            ApplyRotation(rotatevelocity);
            if (!leftBoosterParticle.isPlaying)
            {
                leftBoosterParticle.Play();
            }
            
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotatevelocity);        
            if (!rightBoosterParticle.isPlaying)
            {
                rightBoosterParticle.Play();
            }
        }
        else
        {
            rightBoosterParticle.Stop();
            leftBoosterParticle.Stop();
        }
    }
    
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = false;
        transform.Rotate(Vector3.forward  * Time.fixedDeltaTime * rotationThisFrame);
        rb.freezeRotation = true;
    }
}
