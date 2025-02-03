using UnityEngine;
using UnityEngine.InputSystem;

public class Quit : MonoBehaviour
{
    void Update()
    {
        if(Keyboard.current.escapeKey.isPressed)
        {
            Application.Quit();
        }
    }
}
