using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] GameObject particles = default;

    //Goal Color
    bool goalIsRed;
    bool goalIsGreen;
    bool goalIsBlue;

    //Colliding Player Color
    bool isRed;
    bool isGreen;
    bool isBlue;

    //Goal State
    bool colorsMatch = false;

    private void Start()
    {
        CheckGoalColor();
    }

    private void CheckGoalColor()
    {
        if (GetComponent<Red>())
        {
            goalIsRed = GetComponent<Red>();
        }
        else if (GetComponent<Green>())
        {
            goalIsGreen = GetComponent<Green>();
        }
        else if (GetComponent<Blue>())
        {
            goalIsBlue = GetComponent<Blue>();
        }
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        CheckPlayerColor(player);
        CheckIfColorMatchAndChangeState();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        colorsMatch = false;
    }

    private void CheckIfColorMatchAndChangeState()
    {
        if (goalIsRed && isRed)
        {
            colorsMatch = true;
            ParticlesHandle();
        }
        if (goalIsGreen && isGreen)
        {
            colorsMatch = true;
            ParticlesHandle();
        }
        if (goalIsBlue && isBlue)
        {
            colorsMatch = true;
            ParticlesHandle();
        }
    }

    private void ParticlesHandle()
    {
        Vector3 particlesPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        GameObject particleSystem = Instantiate(particles, particlesPos, Quaternion.identity);
        Destroy(particleSystem, 3f);
    }

    private void CheckPlayerColor(Collider2D player)
    {
        if (player.GetComponent<Red>())
        {
            isRed = player.GetComponent<Red>();
        }
        else if (player.GetComponent<Green>())
        {
            isGreen = player.GetComponent<Green>();
        }
        else if (player.GetComponent<Blue>())
        {
            isBlue = player.GetComponent<Blue>();
        }
    }

    public bool GetColorsMatch()
    {
        return colorsMatch;
    }
}
