using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject[] levels;
    public bool[] unlockedLevel;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        for(int i = 0; i < levels.Length; i++)
        {
            if(unlockedLevel[i])
            {
                levels[i].GetComponent<Button>().interactable = true;
            }
        }
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
}
