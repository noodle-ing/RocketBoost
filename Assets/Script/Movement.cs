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
    float trustStrenght = 0f;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        thrust.Enable();
    }

    private void FixedUpdate()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * trustStrenght * Time.fixedDeltaTime);
        }
    }
}
