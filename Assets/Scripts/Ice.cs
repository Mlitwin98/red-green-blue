using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    PlayerMovement playerSliding;
    void OnTriggerEnter2D(Collider2D other) {
        playerSliding = other.GetComponent<PlayerMovement>();
        playerSliding.StopMovement();
        playerSliding.transform.position = transform.position;
        playerSliding.SetStoppedFaster(false);
        playerSliding.HandleMove(playerSliding.GetLastMove());
        playerSliding.DecreaseMoves();      
    }

    void OnTriggerExit(Collider other) {
        playerSliding = other.GetComponent<PlayerMovement>();
        playerSliding.SetStoppedFaster(true);
    }
}
