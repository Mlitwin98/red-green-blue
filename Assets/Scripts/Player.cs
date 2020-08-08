using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool[] unlockedLevel;
    public int[] starPerLevel;
    public int levelNow;

    private void Awake()
    {
        Singleton();
        starPerLevel = new int[unlockedLevel.Length];
    }

    public void LevelFinished(int level)
    {
        unlockedLevel[level] = true;
    }

    private void Singleton()
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
        starPerLevel[levelNow-1] = stars;
    }
}
