using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    [SerializeField] GameObject winCanvas = default;
    [SerializeField] AudioClip winSFX = default;
    public int level;

    //SIZE 2
    public int[] movePerStar;


    Goal[] goalsArray;
    Player player;
    PlayerMovement playerMovement;
    bool shouldWin = false;
    bool oneTimer = true;

    void Start()
    {
        winCanvas.SetActive(false);
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
            //StartCoroutine(HandleWin());
            oneTimer = false;
        }
    }
    private void HandleWin()
    {
        winCanvas.SetActive(true);
        AudioSource.PlayClipAtPoint(winSFX, Camera.main.transform.position);
        CountStars();
        LevelBeaten();
        //yield return StartCoroutine(WaitForKeyPress());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator WaitForKeyPress()
    {
        do
        {
            yield return null;
        } while (!Input.anyKey);
    }

    private void CountStars()
    {
        if (playerMovement.GetNumMoves() <= movePerStar[0])
        {
            player.UpdateStarPerLever(3);
        }
        else if (playerMovement.GetNumMoves() > movePerStar[0] && playerMovement.GetNumMoves() <= movePerStar[1])
        {
            player.UpdateStarPerLever(2);
        }
        else
        {
            player.UpdateStarPerLever(1);
        }
    }

    private void LevelBeaten()
    {
        FindObjectOfType<Player>().LevelFinished(level);
    }

    public bool GetAlreadyWon()
    {
        return shouldWin;
    }
}
