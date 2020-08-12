using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public Animator fadeAnim;

    public void LoadLevel(int level)
    {
        FindObjectOfType<Player>().levelNow = level;
        StartCoroutine(HandleAnim(level));

    }

    public void LoadLevel(string level)
    {
        StartCoroutine(HandleAnim(level));
    }

    private IEnumerator HandleAnim(int level)
    {
        fadeAnim.SetBool("fadeOut", true);
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(level);
    }
    private IEnumerator HandleAnim(string level)
    {
        fadeAnim.SetBool("fadeOut", true);
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(level);
    }


    public void LoadMainMenu()
    {
        LoadLevel(0);
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
        LoadLevel("Levels");
    }

    public void LoadAuthorScene()
    {
        LoadLevel("Author");
    }

    public void ReloadScene()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
