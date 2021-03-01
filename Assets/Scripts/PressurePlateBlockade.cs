using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateBlockade : MonoBehaviour
{
    [SerializeField] PressurePlate[] pressurePlates = default;

    bool allPressed;
    void Update() {
        if(allPressed)
        {
            Destroy(gameObject);
        }
    }

    public void CheckIfAllPressed()
    {
        foreach(var plate in pressurePlates)
        {
            if(!plate.GetIsPressed())
            {
                allPressed = false;
                break;
            }
            allPressed = true;
        }
    }
}
