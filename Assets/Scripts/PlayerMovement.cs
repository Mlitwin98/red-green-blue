using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpLength = 2f;

    Rigidbody2D rb;

    Vector2 movement;
    Vector2 lastPos;
    public int numMoves;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        numMoves = 0;
    }

    private void Update()
    {
        if (!FindObjectOfType<WinCondition>().GetAlreadyWon())
        {
            CheckForInputs();
        }        
    }

    private void CheckForInputs()
    {
        if (Input.GetKeyDown("w"))
        {
            movement = new Vector2(0, jumpLength);
            SafeCurrentPosition();
            Move();
        }
        if (Input.GetKeyDown("s"))
        {
            movement = new Vector2(0, -jumpLength);
            SafeCurrentPosition();
            Move();
        }
        if (Input.GetKeyDown("a"))
        {
            movement = new Vector2(-jumpLength, 0);
            SafeCurrentPosition();
            Move();
        }
        if (Input.GetKeyDown("d"))
        {
            movement = new Vector2(jumpLength, 0);
            SafeCurrentPosition();
            Move();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position = lastPos;
        numMoves -= 1;
    }
    private void SafeCurrentPosition()
    {
        lastPos = transform.position;
    }
    private void Move()
    {
        Vector2 newPos = new Vector2(transform.position.x + movement.x, transform.position.y + movement.y);
        rb.MovePosition(newPos);
        numMoves += 1;
    }
}
