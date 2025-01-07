using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
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
    float rotatevelocity = 50f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        }
    }
    
    void  ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            ApplyRotation(-1);
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(1);        
        }
    }
    
    void ApplyRotation(int rotationThisFrame)
    {
        transform.Rotate(-Vector3.forward * rotatevelocity * Time.fixedDeltaTime * rotationThisFrame);
    }
    
}
