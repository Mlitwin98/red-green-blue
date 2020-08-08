using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void LoadLevel(int level)
    {
        FindObjectOfType<Player>().levelNow = level;
        SceneManager.LoadScene(level); 
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel()
    {
        LoadLevel(FindObjectOfType<Player>().levelNow + 1);
    }

    public void LoadLevelFromMenu()
    {
        LoadLevel(FindObjectOfType<Player>().levelNow + 1);
    }

    public void LoadLevelsScene()
    {
        SceneManager.LoadScene("Levels");
    }

    public void LoadAuthorScene()
    {
        SceneManager.LoadScene("Author");
    }
}
