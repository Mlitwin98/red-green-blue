using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour
{
    public GameObject[] levels;
    [SerializeField] Sprite disabledSprite = default;

    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (player.unlockedLevel[i])
            {
                levels[i].GetComponent<Button>().interactable = true;
                ActivateStars(i);
            }
            else
            {
                levels[i].GetComponent<Button>().interactable = false;
                levels[i].GetComponent<Image>().sprite = disabledSprite;
            }
        }
    }

    //DRAMAT
    //INSTANTIATE EWENTUALNIE?
    private void ActivateStars(int i)
    {
        if (player.starPerLevel[i] == 1)
        {
            levels[i].transform.Find("Star1").GetComponent<Image>().enabled = true;
        }
        else if (player.starPerLevel[i] == 2)
        {
            levels[i].transform.Find("Star1").GetComponent<Image>().enabled = true;
            levels[i].transform.Find("Star2").GetComponent<Image>().enabled = true;
        }
        else if (player.starPerLevel[i] == 3)
        {
            levels[i].transform.Find("Star1").GetComponent<Image>().enabled = true;
            levels[i].transform.Find("Star2").GetComponent<Image>().enabled = true;
            levels[i].transform.Find("Star3").GetComponent<Image>().enabled = true;
        }
        else
        {
            levels[i].transform.Find("Star1").GetComponent<Image>().enabled = false;
            levels[i].transform.Find("Star2").GetComponent<Image>().enabled = false;
            levels[i].transform.Find("Star3").GetComponent<Image>().enabled = false;
        }
    }
}
