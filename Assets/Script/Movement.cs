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
    float trustStrenght ;

    [SerializeField]  
    InputAction rotation;

    [SerializeField] 
    float rotatevelocity;
    
    [SerializeField] 
    ParticleSystem mainBoosterParticle;
    [FormerlySerializedAs("LeftBoosterParticle")] 
    [SerializeField] 
    ParticleSystem leftBoosterParticle;
    [FormerlySerializedAs("RightBoosterParticle")] 
    [SerializeField] 
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
            Thrust();
        }
        else
        {
            StopThrust();
        }
    }
    
    
    void  ProcessRotation()
    {
        Rotation();
    }
    
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = false;
        transform.Rotate(Vector3.forward  * Time.fixedDeltaTime * rotationThisFrame);
        rb.freezeRotation = true;
    }

    void Thrust()
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

    void StopThrust()
    {
        flyAudio.Stop();
        mainBoosterParticle.Stop();
    }

    void Rotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            LeftRotation();
        }
        else if (rotationInput > 0)
        {
            RightRotation();
        }
        else
        {
            StopRotation();
        }
    }

    void StopRotation()
    {
        rightBoosterParticle.Stop();
        leftBoosterParticle.Stop();
    }

    void RightRotation()
    {
        ApplyRotation(-rotatevelocity);        
        if (!rightBoosterParticle.isPlaying)
        {
            rightBoosterParticle.Play();
        }
    } 
    
    void LeftRotation()
    {
        ApplyRotation(rotatevelocity);
        if (!leftBoosterParticle.isPlaying)
        {
            leftBoosterParticle.Play();
        }
    }
}
