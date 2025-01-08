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
    float rotatevelocity =30f;

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
            ApplyRotation(rotatevelocity);
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotatevelocity);        
        }
    }
    
    void ApplyRotation(float rotationThisFrame)
    {
        transform.Rotate(Vector3.forward  * Time.fixedDeltaTime * rotationThisFrame);
    }
}
