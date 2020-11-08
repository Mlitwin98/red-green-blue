using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool[] unlockedLevel;
    public int[] starPerLevel;
    int currentLevel;

    void Awake()
    {
        Singleton();
        starPerLevel = new int[unlockedLevel.Length];
    }

    public void LevelFinished(int level)
    {
        unlockedLevel[level] = true;
    }

    void Singleton()
    {
        if (FindObjectsOfType<Player>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UpdateStarPerLever(int stars)
    {
        starPerLevel[currentLevel-1] = stars;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void SetCurrentLevel(int set)
    {
        currentLevel = set;
    }

    public void IncrementCurrentLevel()
    {
        currentLevel++;
    }
}
