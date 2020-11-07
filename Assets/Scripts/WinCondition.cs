using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    [SerializeField] GameObject winCanvasFadeToAnimate = default;
    [SerializeField] AudioClip winSFX = default;
    public int level;

    //SIZE 2
    public int[] movePerStar;


    Goal[] goalsArray;
    Player player;
    PlayerMovement playerMovement;
    bool shouldWin = false;
    bool oneTimer = true;
    int stars;

    void Start()
    {
        goalsArray = FindObjectsOfType<Goal>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        player = FindObjectOfType<Player>();
    }

    void FixedUpdate()
    {
        foreach (Goal goal in goalsArray)
        {
            if (goal.GetColorsMatch())
            {
                shouldWin = true;
            }
            else
            {
                shouldWin = false;
                break;
            }
        }
        if (shouldWin && oneTimer)
        {
            HandleWin();
            oneTimer = false;
        }
    }
    void HandleWin()
    {
        CountStars();
        AnimateWinCanvas();
        AudioSource.PlayClipAtPoint(winSFX, Camera.main.transform.position);
        if (player != null )
        {
            SaveStars();
            LevelBeaten();
        }
    }

    void AnimateWinCanvas()
    {
        winCanvasFadeToAnimate.GetComponent<Animator>().SetBool("won", true);
        Transform starsParent = winCanvasFadeToAnimate.transform.Find("Stars").transform;
        for (int i = 0; i < stars; i++)
        {
            starsParent.GetChild(i).GetComponent<Image>().enabled = true;
        }
    }

    void CountStars()
    {
        if (playerMovement.GetNumMoves() <= movePerStar[0])
        {
            stars = 3;
        }
        else if (playerMovement.GetNumMoves() > movePerStar[0] && playerMovement.GetNumMoves() <= movePerStar[1])
        {
            stars = 2;
        }
        else
        {
            stars = 1;
        }
    }

    void SaveStars()
    {
        player.UpdateStarPerLever(stars);
    }

    void LevelBeaten()
    {
        player.LevelFinished(level);
    }

    public bool GetAlreadyWon()
    {
        return shouldWin;
    }
}
