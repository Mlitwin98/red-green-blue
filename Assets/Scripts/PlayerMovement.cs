using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpLength = 1.5f;

    Rigidbody2D rb;
    Vector2 movement;
    Vector2 lastPos;
    public int numMoves;
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    private Animator animator;

    public float SWIPE_THRESHOLD = 20f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        numMoves = 0;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!FindObjectOfType<WinCondition>().GetAlreadyWon())
        {
            CheckForInputs();
            MobileInputs();
        }
    }

    private void CheckForInputs()
    {
        if (Input.GetKeyDown("w"))
        {
            HandleMove(new Vector2(0, jumpLength));
        }
        if (Input.GetKeyDown("s"))
        {
            HandleMove(new Vector2(0, -jumpLength));
        }
        if (Input.GetKeyDown("a"))
        {
            HandleMove(new Vector2(-jumpLength, 0));
        }
        if (Input.GetKeyDown("d"))
        {
            HandleMove(new Vector2(jumpLength, 0));
        }
    }

    private void HandleMove(Vector2 dir)
    {
        movement = dir;
        animator.SetTrigger("move");
        SafeCurrentPosition();
        Move();
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

    private void MobileInputs()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }
        }
    }

    void checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnSwipeUp();
            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                OnSwipeDown();
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    void OnSwipeUp()
    {
        HandleMove(new Vector2(0, jumpLength));
    }

    void OnSwipeDown()
    {
        HandleMove(new Vector2(0, -jumpLength));
    }

    void OnSwipeLeft()
    {
        HandleMove(new Vector2(-jumpLength, 0));
    }

    void OnSwipeRight()
    {
        HandleMove(new Vector2(jumpLength, 0));
    }
}
