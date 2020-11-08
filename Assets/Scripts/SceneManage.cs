using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    [SerializeField] Animator fadeCanvas = default;
    Player player;

    void Start() 
    {
        player = FindObjectOfType<Player>();
    }

    public void LoadLevel(int level)
    {
        if (player != null) player.SetCurrentLevel(level);
        StartCoroutine(HandleAnim(level));
    }

    public void LoadLevel(string level)
    {
        StartCoroutine(HandleAnim(level));
    }

    IEnumerator HandleAnim(int level)
    {
        fadeCanvas.SetBool("fadeOut", true);
        yield return new WaitForSeconds(fadeCanvas.GetCurrentAnimatorStateInfo(0).length+0.1f);
        SceneManager.LoadScene(level);
    }
    IEnumerator HandleAnim(string level)
    {
        fadeCanvas.SetBool("fadeOut", true);
        yield return new WaitForSeconds(fadeCanvas.GetCurrentAnimatorStateInfo(0).length+0.1f);
        SceneManager.LoadScene(level);
    }


    public void LoadMainMenu()
    {
        LoadLevel(0);
    }

    public void LoadNextLevel()
    {
        LoadLevel(player.GetCurrentLevel() + 1);
    }

    public void LoadLevelFromMenu()
    {
        LoadLevel(player.GetCurrentLevel() + 1);
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


    //NO ANIMATION
    public void LoadLevelsWithoutAnim()
    {
        SceneManager.LoadScene("Levels");
    }

    public void LoadReloadWithoutAnim()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenuWithoutAnim()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevelWithoutAnim()
    {
        int nextLevel = player.GetCurrentLevel() + 1;
        player.SetCurrentLevel(nextLevel);
        SceneManager.LoadScene(nextLevel + 1);
    }
}
