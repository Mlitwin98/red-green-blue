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
    Vector2 lastMove;
    int numMoves;
    Vector2 fingerDown;
    Vector2 fingerUp;
    Coroutine movementCoroutine;
    bool hitCollision;
    bool waitingForInput = true;

    static bool stoppedFaster = false;
    static int stoppedMoving = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        numMoves = 0;
    }

    void Update()
    {
        if (!FindObjectOfType<WinCondition>().GetAlreadyWon() && waitingForInput && !stoppedFaster)
        {
            hitCollision = false;
            CheckForInputs();
            MobileInputs();
        }
        Debug.Log(stoppedFaster);
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
        stoppedFaster = true;
        stoppedMoving += 1;
    }

    public void HandleMove(Vector2 dir)
    {
        lastMove = dir;
        SafeCurrentPosition();
        movementCoroutine = StartCoroutine(Move(dir));
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

        //movement bug fix
        if(!hitCollision)
        {
            stoppedFaster = false;
        }
        if(stoppedMoving >= 3)
        {
            stoppedFaster = false;
            stoppedMoving = 0;
        }

        numMoves += 1;
        waitingForInput = true;

        //TO FIX:
        //Can't move after collision after sliding on ice
        //Can move asynch in some situations
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

    public void SetHitCollision(bool set)
    {
        hitCollision = set;
    }

    public Vector2 GetLastMove()
    {
        return lastMove;
    }

    public void DecreaseMoves()
    {
        numMoves -= 1;
    }

    public void StopMovement()
    {
        StopCoroutine(movementCoroutine);
    }

    public void SetStoppedFaster(bool set)
    {
        stoppedFaster = set;
    }
}
