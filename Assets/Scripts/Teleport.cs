using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] Teleport teleportExit = default;

    bool canTP = true;
    void OnTriggerEnter2D(Collider2D other) {
        if(canTP){
            teleportExit.canTP = false;
            other.transform.position = teleportExit.transform.position;
            other.GetComponent<PlayerMovement>().SetHitCollision(true);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        canTP = true;
    }
}
