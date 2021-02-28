using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    PlayerMovement playerSliding;
    void OnTriggerEnter2D(Collider2D other) {
        playerSliding = other.GetComponent<PlayerMovement>();
        playerSliding.StopMovement();
        playerSliding.HandleSlide(transform.position, playerSliding.GetLastMove());   
    }
}
