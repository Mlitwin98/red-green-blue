using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    bool isPressed = false;
    PressurePlateBlockade obstacle;

    private void OnTriggerEnter2D(Collider2D other) {
        isPressed = true;
        if(obstacle = FindObjectOfType<PressurePlateBlockade>()) obstacle.CheckIfAllPressed();
    }

    void OnTriggerExit2D(Collider2D other) {
        isPressed = false;
    }

    public bool GetIsPressed()
    {
        return isPressed;
    }
}