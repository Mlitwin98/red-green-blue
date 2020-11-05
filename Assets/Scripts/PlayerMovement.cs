using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpLength = 1.5f;
    [SerializeField] float SWIPE_THRESHOLD = 20f;
    [SerializeField] float speed = 5f;
    Rigidbody2D rb;
    Vector2 lastPos;
    int numMoves;
    Vector2 fingerDown;
    Vector2 fingerUp;
    bool hitCollision;
    bool waitingForInput = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        numMoves = 0;
    }

    void Update()
    {
        if (!FindObjectOfType<WinCondition>().GetAlreadyWon() && waitingForInput)
        {
            hitCollision = false;
            CheckForInputs();
            MobileInputs();
        }
    }

    void CheckForInputs()
    {
        if (Input.GetKeyDown("w"))
        {
            HandleMove(new Vector2(0, jumpLength));
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetKeyDown("s"))
        {
            HandleMove(new Vector2(0, -jumpLength));
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        if (Input.GetKeyDown("a"))
        {
            HandleMove(new Vector2(-jumpLength, 0));
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetKeyDown("d"))
        {
            HandleMove(new Vector2(jumpLength, 0));
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        hitCollision = true;
        transform.position = lastPos;
        numMoves -= 1;  
    }

    void HandleMove(Vector2 dir)
    {
        SafeCurrentPosition();
        StartCoroutine(Move(dir));
    }
    void SafeCurrentPosition()
    {
        lastPos = transform.position;
    }
    IEnumerator Move(Vector2 movement)
    {
        Vector3 newPos = new Vector3(transform.position.x + movement.x, transform.position.y + movement.y, transform.position.z);
        while (transform.position != newPos && !hitCollision)
        {
            waitingForInput = false;
            transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        numMoves += 1;
        waitingForInput = true;
    }

    void MobileInputs()
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
                CheckSwipe();
            }
        }
    }

    void CheckSwipe()
    {
        //Check if Vertical swipe
        if (VerticalMove() > SWIPE_THRESHOLD && VerticalMove() > HorizontalValMove())
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
        else if (HorizontalValMove() > SWIPE_THRESHOLD && HorizontalValMove() > VerticalMove())
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

    float VerticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float HorizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    void OnSwipeUp()
    {
        HandleMove(new Vector2(0, jumpLength));
        transform.rotation = Quaternion.Euler(0, 0, 90);
    }

    void OnSwipeDown()
    {
        HandleMove(new Vector2(0, -jumpLength));
        transform.rotation = Quaternion.Euler(0, 0, -90);
    }

    void OnSwipeLeft()
    {
        HandleMove(new Vector2(-jumpLength, 0));
        transform.rotation = Quaternion.Euler(0, 0, 180);
    }

    void OnSwipeRight()
    {
        HandleMove(new Vector2(jumpLength, 0));
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public int GetNumMoves()
    {
        return numMoves;
    }
}
