using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject[] buttons;

    // 0 - normal, 1 - sleeping
    public Sprite[] red, green, blue;

    private List<Image> imageComponent = new List<Image>();
    private void Start()
    {
        foreach(GameObject button in buttons)
        {
            imageComponent.Add(button.GetComponent<Image>());
        }

        imageComponent[0].sprite = red[0];
        imageComponent[1].sprite = green[1];
        imageComponent[2].sprite = blue[1];
    }

    public void RedClicked()
    {
        imageComponent[0].sprite = red[0];
        imageComponent[1].sprite = green[1];
        imageComponent[2].sprite = blue[1];
    }

    public void GreenClicked()
    {
        imageComponent[0].sprite = red[1];
        imageComponent[1].sprite = green[0];
        imageComponent[2].sprite = blue[1];
    }

    public void BlueClicked()
    {
        imageComponent[0].sprite = red[1];
        imageComponent[1].sprite = green[1];
        imageComponent[2].sprite = blue[0];
    }
}
