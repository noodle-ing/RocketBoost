using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    
    void Update()
    {
        Quit();
    }

    void Quit()
    {
        if (Keyboard.current.escapeKey.IsPressed())
        {
            Application.Quit();
            Debug.Log("You quit the game");
        }
    }
}
