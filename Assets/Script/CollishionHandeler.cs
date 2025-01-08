using System;
using UnityEngine;

public class CollishionHandeler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Frendly":
                break;
            case "Finish":
                break;
            case "Fuel":
                Debug.Log("circke");
                break;
            default:
                Debug.Log("you crashed");
                break;
        }

       
    }
}
